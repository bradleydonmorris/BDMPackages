using BDMCiphers;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SARTeam.Security
{
    public class DeviceToken
    {
        private const String PRIVATE_ENCRYPTION_KEY = "<RSAKeyValue><Modulus>13CZLEiRZnrTYlYUHVRS2DAUKnk5cv8ZS/LhTpZpgb2BFR8+BzGqXvnrPoKgTrUG36Rs3W2p2KTiG+xzTqeRz7jZHw/pU2rXBRgZApoRcWfTefhITFSFOm7jX0/4sToY0xNpu3yjDEjmFZs95aMM2RbzlS58qzM5guPr01jnugXE/nxdr0Iy25yhAdHOsMDCJHF7Gl2fiG4jbHLmvq6QO0UZdBjoxvxfgxH5+E3JHEW47lJ2XqODVN9q4RwJxjuDqtsryAoT9VBAakTww9znsW6e1LKOXKLAvAW3uj58nABKXIrChof/ozE0RpkZa1pMx6GLyGvcA3ta5n3kaxk8dQ==</Modulus><Exponent>AQAB</Exponent><P>3oychhM/JDpwRYYeV7Mb77Dw76DyWpP0S5I3emODqVosIuyy2qMIrtydnUf9MJsFkTXOia65MKIZau0roC0QrlQO+LVLuXQCKZ3wUJ0T+XS/uREvIS2Ndhy10QCN14sL606A43bAGFD4p4GsC+jKJ1Z2+ErbAz91xBV6yO3gqps=</P><Q>99JtDo5FEvVogtSorvmTaTV6FWJAkH4MwuMShsSDqAXsYqp5RkAjHTvlqNGJ6cSPlKkvRmgLinFmAAY0kMpp1OJJmgGxOkDeWBGwVnOwsSeLtf8BF22m0g9QR22KtUo6w3zoJYPjOsgyovnNJbt2gpTm2lnwTTtk+x7Mr8VvXi8=</Q><DP>PCLIxN38iOCVp+O23UHTwbARWt8lvd4O/EC1Zwfu8tsf5AwLxFKbrQyGNpLWUsUA+x0MA15IQmD6907BJBYpVMH4DbKHlMRJNU+tOUtIs3adu/dtwcxaVlkptCEvMtgqz1m2MEDNZYSzvCepAsrTnU+a9Drd9YG3Y9XeCN9mfxM=</DP><DQ>3jAVgwVfPL42M4aCrk4pMhy2FlH+3Q2GV3zK8XVjLNQSuetpy+hZEy7Om2syoRqQOJLvRcqm/jrpXoAxxNcVx74SizeVNEtQjdleJkSXWF3CTywQSHcRKCh+q2u0/xFMWlS0tl3m7p7rd19L5NaMTEtAJuiYIEZuJQ/aKIV1chU=</DQ><InverseQ>hFWet/IHgUUqtDS+NpcEQnfo2HDWgH1z6joh9EcnVPx5btQGToYTLXc8Ak5FkKuAEdgMe08fOkaDD9Yy8YlEct5K7otmZUniicpWG7N4FFDWWrlPIaY+tHecM1co1wvF7EesssfslhjACkOnfzYmFxX8GqYhilxNUMI5kA74rPg=</InverseQ><D>p9zb/F0uraLWQbDAS3oAcuJDVDTQRoHtu/erBgi8fJKjmUmWkxRno0knurUdIzZ5/JepG37u8BbI9ujtj0ORoUBuLH3BV3XPw/BazGQvYyV66XZDHvZcOINeNk2hgLW70fs2Txd2E4RZgkxLljnNMPlpIbvu95YaIZpljW+gXpfhves6LLLy/YrDrKQB5OmMDwAJ01M6+WtVAp+jBBecKjo5m9ICjcdelZBLJjG4xNzwOOnEBgvYDNg4GYxcCfzn1eBMU7TehFaNTe/kXN9t+KJJ/dtv5h+lYM3T2QlWIolMZXDZmd67cW/aEqLxfXjEJMu/3bqh/frmVZ2BoNnsoQ==</D></RSAKeyValue>";
        private const String PUBLIC_ENCRYPTION_KEY = "<RSAKeyValue><Modulus>13CZLEiRZnrTYlYUHVRS2DAUKnk5cv8ZS/LhTpZpgb2BFR8+BzGqXvnrPoKgTrUG36Rs3W2p2KTiG+xzTqeRz7jZHw/pU2rXBRgZApoRcWfTefhITFSFOm7jX0/4sToY0xNpu3yjDEjmFZs95aMM2RbzlS58qzM5guPr01jnugXE/nxdr0Iy25yhAdHOsMDCJHF7Gl2fiG4jbHLmvq6QO0UZdBjoxvxfgxH5+E3JHEW47lJ2XqODVN9q4RwJxjuDqtsryAoT9VBAakTww9znsW6e1LKOXKLAvAW3uj58nABKXIrChof/ozE0RpkZa1pMx6GLyGvcA3ta5n3kaxk8dQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private const String PRIVATE_SIGNING_KEY = "<RSAKeyValue><Modulus>1E7oRRc/WMEC/D6i5Pkfvi/b3btkJiUahgoOrKfu0jwzAmHK4yu6qn5d8OZKPXj/ouD82GQeqJFLVy0mzUkJeqXCi6kB+d+h2c2zPBSSgLFvHksom+SuBBDkdpthqXnDAZ+/9FQ7E5gkYfXoOhwdx6GuyuGFB6iN6p0NwXvOzULDks3Mc1Jaw0A9wy99d5DrzS9BqnlWZWrHlh0FdrbE0/iVbpnDprUBazYZovvql3yaTXqV0vndRUj1dXcrf+nNDtByJtnpP4YaZDJkaijtL6JiSiO3sZn0uUDRwFXU7C+umRRV8oxPJua4QNx5erYC+wzhKTJcCsuzA3dto9PXJQ==</Modulus><Exponent>AQAB</Exponent><P>+FQblkoqb2T7IWyk+KPvbLdxMhawbQYb2qF9owMJBP4GmyPbn9e2KwOe22pXwZ38s1OkVn8DV3CvJgGU8NTJdRV2uebuVXg8zpwCMaFBIm1XDRjX8g75kC81tEglnKO4OVrMjBVcwzGlm4VArw46VuciMIbOpWBi0cOZTW0M5rc=</P><Q>2t3vV2QBsi4emky1n3xziu6Lm/SO3t/xSCJ0i2pT8XtTCy2VsTpsGLuTkY1oieg/3OK2Psj8QssnX1jmZO02u0/coHL7u8DbsJC4gMUQtyvAiaro2Sf86f0gBgMFxhb9n80GCsxN3ZkkqvDXCCCpuxbUNzHuZXvY2zCMmTsg9QM=</Q><DP>yNP0b4nRYb8v3asoi7DAq5J7Z/+zdKhRFHIv0gpdW/04SHUqY82nFIhcC6SoDfx/5mMJb65OfIO0Ei+LTW0484iFOPkK3HJd8tV2bL61l2sH0J673ZXAvJuBeigyysgY78F/1PZdy7o17V1Jn4kJI0jPfKE5M8OMh4oBS/2aRmc=</DP><DQ>HeLSTNeeF6uOiDlTT6zZxi3eqHQTkkF4HAaAbY0eW0ogw9T8rM+ydQogE28AyFwLZGkcw6QENkaxYKauBzgREjDMiqhI0ZF7LjgEyaifKLPzvZi15Pk94uGqnseI4UzAYzLG0XCmbUz9Ic5zPmvfYWmznrnVuMvZh8ywVjgpvus=</DQ><InverseQ>GvHdnMhvIpereuK4u1i8Sa8rmliaPn9s4tn+Kckzb4oLPYdhhUaYZFzNt2yCmtWHU2mZexQpyECxNm7DFILzEkf/lFB+qF8Bqw13laDd2i0GKe+tO5yvT+CUdDEqYApwqqcYybRKXbQANjSuwYwHjkm4d0gXpdukAmgTMpuUU1k=</InverseQ><D>pMCK/DqbHNvC3k4ZCIjOej8XZozpnbRDTer2EkPLT3gJuXp8ACfKFk4zxiJfUC7aaeIgNCgVpogBqCfQqZNY+MUzID/J2ccSCTvYL/Ji8d0/NonqfHdMFrmpIOVZNzPXASafxhGrOnxhF2fFi3qQHX+5bZTuL9A8OOkhaJkUFTGkl+IBD4qm7Felp81/H1T414YONWzMAqR/p9w4vPfk62vUcJMXv3XUHG1JjCObP76rVeCuqyqj5OvaVQZCirqDqP76/jzNLcfWoIeRK+u2NqeEYsfXbR/5lOtGa9TBguVhnh8ADzCiprYoE8r2X/IOCchr+rn/gpufKDoC/ruCTQ==</D></RSAKeyValue>";
        private const String PUBLIC_SIGNING_KEY = "<RSAKeyValue><Modulus>1E7oRRc/WMEC/D6i5Pkfvi/b3btkJiUahgoOrKfu0jwzAmHK4yu6qn5d8OZKPXj/ouD82GQeqJFLVy0mzUkJeqXCi6kB+d+h2c2zPBSSgLFvHksom+SuBBDkdpthqXnDAZ+/9FQ7E5gkYfXoOhwdx6GuyuGFB6iN6p0NwXvOzULDks3Mc1Jaw0A9wy99d5DrzS9BqnlWZWrHlh0FdrbE0/iVbpnDprUBazYZovvql3yaTXqV0vndRUj1dXcrf+nNDtByJtnpP4YaZDJkaijtL6JiSiO3sZn0uUDRwFXU7C+umRRV8oxPJua4QNx5erYC+wzhKTJcCsuzA3dto9PXJQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private const String PASSPHRASE = "4wY2SVrcn76f#R74";

        [JsonProperty("User")]
        public String? User { get; set; }

        [JsonProperty("OSUser")]
        public String? OSUser { get; set; }

        [JsonProperty("Host")]
        public String? Host { get; set; }

        [JsonProperty("Assembly")]
        public String? Assembly { get; set; }

        [JsonProperty("CreateTime")]
        public DateTime? CreateTime { get; set; }

        public DeviceToken() { }

        public DeviceToken(String user, String osUser, String host, String assembly)
        {
            this.User = user;
            this.OSUser = osUser;
            this.Host = host;
            this.Assembly = assembly;
            this.CreateTime = DateTime.UtcNow;
        }

        public Envelope Seal()
        {
            Envelope returnValue = new(
                JsonConvert.SerializeObject
                (
                    this,
                    new JsonSerializerSettings()
                    {
                        DateFormatString = "yyyy-MM-dd HH:mm:ss.fffffffK",
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Include
                    }
                ),
                PASSPHRASE
            );
            returnValue.Seal(PUBLIC_ENCRYPTION_KEY, PRIVATE_SIGNING_KEY);
            return returnValue;
        }

        public static DeviceToken? Open(Envelope? envelope)
        {
            if (envelope is not null)
            {
                envelope.Open(PRIVATE_ENCRYPTION_KEY, PUBLIC_SIGNING_KEY);
                if (envelope.State == EnvelopeState.Opened)
                    return JsonConvert.DeserializeObject<DeviceToken>(envelope.Content);
                else
                    return null;
            }
            else
                return null;
        }
    }
}
