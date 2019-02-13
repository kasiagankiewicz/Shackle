using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.ECDSA;
using Shackle.Core.Models;
using Shackle.Core.Services;

namespace Shackle.Infrastructure.Crypto
{
    public class Signer : ISigner
    {
        public Signature Sign(IEnumerable<byte> data, PrivateKey privateKey)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data), "Data can not be null");
            }

            if (privateKey is null)
            {
                throw new ArgumentNullException(nameof(privateKey), "Private key can not be null");
            }

            var signature = Secp256K1Manager.SignCompressedCompact(data.ToArray(), privateKey.Bytes);

            return new Signature(signature);
        }
    }
}