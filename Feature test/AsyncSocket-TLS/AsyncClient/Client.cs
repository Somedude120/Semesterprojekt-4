using System;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

public class SecureTcpClient : IDisposable
{
    X509CertificateCollection clientCertificates;
    RemoteCertificateValidationCallback certValidationCallback;
    SecureConnectionResultsCallback connectionCallback;
    bool checkCertificateRevocation;

    AsyncCallback onConnected;
    AsyncCallback onAuthenticateAsClient;
    TcpClient client;
    IPEndPoint remoteEndPoint;
    string remoteHostName;
    SslProtocols protocols;
    int disposed;

    public SecureTcpClient(SecureConnectionResultsCallback callback) : this(callback, null, SslProtocols.Default)
    {
    }

    public SecureTcpClient(SecureConnectionResultsCallback callback,
        RemoteCertificateValidationCallback certValidationCallback) : this(callback, certValidationCallback, SslProtocols.Default)
    {
    }

    public SecureTcpClient(SecureConnectionResultsCallback callback,
        RemoteCertificateValidationCallback certValidationCallback,
        SslProtocols sslProtocols)
    {
        if (callback == null)
            throw new ArgumentNullException("callback");

        onConnected = new AsyncCallback(OnConnected);
        onAuthenticateAsClient = new AsyncCallback(OnAuthenticateAsClient);

        this.certValidationCallback = certValidationCallback;
        this.connectionCallback = callback;
        protocols = sslProtocols;
        this.disposed = 0;
    }

    ~SecureTcpClient()
    {
        Dispose();
    }

    public bool CheckCertificateRevocation
    {
        get { return checkCertificateRevocation; }
        set { checkCertificateRevocation = value; }
    }

    public void StartConnecting(string remoteHostName, IPEndPoint remoteEndPoint)
    {
        StartConnecting(remoteHostName, remoteEndPoint, null);
    }

    public void StartConnecting(string remoteHostName, IPEndPoint remoteEndPoint,
        X509CertificateCollection clientCertificates)
    {
        if (string.IsNullOrEmpty(remoteHostName))
            throw new ArgumentException("Value cannot be null or empty", "remoteHostName");

        if (remoteEndPoint == null)
            throw new ArgumentNullException("remoteEndPoint");

        Console.WriteLine("Client connecting to: {0}", remoteEndPoint);

        this.clientCertificates = clientCertificates;
        this.remoteHostName = remoteHostName;
        this.remoteEndPoint = remoteEndPoint;

        if (client != null)
            client.Close();

        client = new TcpClient(remoteEndPoint.AddressFamily);

        client.BeginConnect(remoteEndPoint.Address,
            remoteEndPoint.Port,
            this.onConnected, null);
    }

    public void Close()
    {
        Dispose();
    }

    void OnConnected(IAsyncResult result)
    {
        SslStream sslStream = null;

        try
        {
            bool leaveStreamOpen = false;//close the socket when done

            if (this.certValidationCallback != null)
                sslStream = new SslStream(client.GetStream(), leaveStreamOpen, this.certValidationCallback);
            else
                sslStream = new SslStream(client.GetStream(), leaveStreamOpen);


            sslStream.BeginAuthenticateAsClient(this.remoteHostName,
                    this.clientCertificates,
                    this.protocols,
                    this.checkCertificateRevocation,
                    this.onAuthenticateAsClient,
                    sslStream);
        }
        catch (Exception ex)
        {
            if (sslStream != null)
            {
                sslStream.Dispose();
                sslStream = null;
            }

            this.connectionCallback(this, new SecureConnectionResults(ex));
        }
    }

    void OnAuthenticateAsClient(IAsyncResult result)
    {
        SslStream sslStream = null;
        try
        {
            sslStream = result.AsyncState as SslStream;
            sslStream.EndAuthenticateAsClient(result);

            this.connectionCallback(this, new SecureConnectionResults(sslStream));
        }
        catch (Exception ex)
        {
            if (sslStream != null)
            {
                sslStream.Dispose();
                sslStream = null;
            }
            this.connectionCallback(this, new SecureConnectionResults(ex));
        }
    }

    public void Dispose()
    {
        if (System.Threading.Interlocked.Increment(ref disposed) == 1)
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}