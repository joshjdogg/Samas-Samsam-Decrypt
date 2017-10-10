using System;
using System.IO;
using System.Security.Cryptography;

namespace SAMDdec
{
	internal class Encipher
	{
		public static string sn;

		public static int chunkSize;

		public static int headerSize;

		static Encipher()
		{
			Encipher.sn = Environment.NewLine;
			Encipher.chunkSize = 1048576;
			Encipher.headerSize = 3072;
		}

		public Encipher()
		{
		}

		public static void DecryptFile(string encryptedFilePath, string decryptedFilePath, byte[] key, byte[] iv, long lOrgFileSize)
		{
			int num;
			if (File.Exists(decryptedFilePath))
			{
				File.Delete(decryptedFilePath);
			}
			long length = (new FileInfo(encryptedFilePath)).Length;
			long num1 = (long)((int)(length / (long)Encipher.chunkSize + (long)1));
			bool file = true;
			try
			{
				for (long i = (long)0; i < num1; i = i + (long)1)
				{
					byte[] bytesFromFile = Encipher.GetBytesFromFile(encryptedFilePath, (long)Encipher.headerSize + i * (long)Encipher.chunkSize, out num);
					if (num > 0)
					{
						byte[] numArray = new byte[num];
						Buffer.BlockCopy(bytesFromFile, 0, numArray, 0, num);
						byte[] numArray1 = Encipher.DecryptStringFromBytes(numArray, key, iv, num);
						file = Encipher.WriteBytesToFile(decryptedFilePath, numArray1);
						if (!file)
						{
							break;
						}
					}
				}
				if (file)
				{
					long num2 = length - (long)Encipher.headerSize - lOrgFileSize;
					FileInfo fileInfo = new FileInfo(decryptedFilePath);
					FileStream fileStream = fileInfo.Open(FileMode.Open);
					fileStream.SetLength(Math.Max((long)0, fileInfo.Length - num2));
					fileStream.Close();
				}
			}
			catch (Exception exception)
			{
			}
		}

		public static byte[] DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV, int size)
		{
			byte[] numArray;
			byte[] numArray1 = new byte[size];
			if (cipherText == null || cipherText.Length == 0)
			{
				throw new ArgumentNullException("cipherText");
			}
			if (Key == null || Key.Length == 0)
			{
				throw new ArgumentNullException("AAA");
			}
			if (IV == null || IV.Length == 0)
			{
				throw new ArgumentNullException("AA");
			}
			using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
			{
				rijndaelManaged.KeySize = 128;
				rijndaelManaged.FeedbackSize = 8;
				rijndaelManaged.Key = Key;
				rijndaelManaged.IV = IV;
				rijndaelManaged.Padding = PaddingMode.Zeros;
				RijndaelManaged rijndaelManaged1 = rijndaelManaged;
				ICryptoTransform cryptoTransform = rijndaelManaged1.CreateDecryptor(rijndaelManaged1.Key, rijndaelManaged.IV);
				using (MemoryStream memoryStream = new MemoryStream(cipherText))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
					{
						cryptoStream.Read(numArray1, 0, size);
					}
				}
				numArray = numArray1;
			}
			return numArray;
		}

		public static byte[] GetBytesFromFile(string fullFilePath, long from, out int readCount)
		{
			byte[] numArray;
			using (FileStream fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				byte[] numArray1 = new byte[Encipher.chunkSize];
				fileStream.Seek(from, SeekOrigin.Begin);
				readCount = fileStream.Read(numArray1, 0, (int)numArray1.Length);
				numArray = numArray1;
			}
			return numArray;
		}

		public static byte[] GetHeaderBytesFromFile(string fullFilePath)
		{
			byte[] numArray;
			using (FileStream fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				byte[] numArray1 = new byte[Encipher.headerSize];
				fileStream.Seek((long)0, SeekOrigin.Begin);
				fileStream.Read(numArray1, 0, (int)numArray1.Length);
				numArray = numArray1;
			}
			return numArray;
		}

		public static string GetStringFromBytes(byte[] bytes)
		{
			char[] chrArray = new char[(int)bytes.Length / 2];
			Buffer.BlockCopy(bytes, 0, chrArray, 0, (int)bytes.Length);
			return new string(chrArray);
		}

		public static byte[] RSADescryptBytes(byte[] datas, string keyXml)
		{
			byte[] numArray = null;
			using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(2048))
			{
				rSACryptoServiceProvider.FromXmlString(keyXml);
				try
				{
					numArray = rSACryptoServiceProvider.Decrypt(datas, true);
				}
				catch (Exception exception)
				{
				}
			}
			return numArray;
		}

		public static bool WriteBytesToFile(string _FileName, byte[] _ByteArray)
		{
			bool flag;
			FileStream fileStream = new FileStream(_FileName, FileMode.Append, FileAccess.Write);
			try
			{
				fileStream.Write(_ByteArray, 0, (int)_ByteArray.Length);
				fileStream.Close();
				flag = true;
			}
			catch (Exception exception)
			{
				Console.WriteLine("Exception caught in process: {0}", exception.ToString());
				fileStream.Close();
				if (File.Exists(_FileName))
				{
					File.Delete(_FileName);
				}
				return false;
			}
			return flag;
		}
	}
}