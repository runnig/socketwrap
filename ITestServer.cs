using System;
using System.Net;
using System.Net.Sockets;

using AsyncServerImpl;
using SyncServerImpl;

namespace socketwrap
{	
	public enum SyncEnum { Async, Sync }
	public interface ITestServer
	{
		public static ITestServer Create(bool async, int max_conn, int port,
			IMessageHandler mh)
		{
			if(async)
			{
				return new AsyncServerImpl(max_conn, port, mh);
			}
			else
			{
				return new SyncServerImpl(max_conn, port, mh);
			}
		}
		void Serve();
	}
	
	public class Message 
	{
		public Socket		m_socket = null;
		public const int	m_buf_size = 1024;
		public byte[]		m_buf = new byte[m_buf_size];
		public StringBuilder m_sb = new StringBuilder();  
		Message(Socket s)
		{
			m_socket = s;
		}
	}

	public class ReplyMessage : Message
	{
		public bool m_quit;
	}
	
	public interface IMessageHandler
	{
		ReplyMessage Handle(Message m);
	}
	

}

