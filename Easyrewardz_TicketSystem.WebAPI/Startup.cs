using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Easyrewardz_TicketSystem.WebAPI
{
    public class Startup
    {
        /// <summary>
        /// Start Up method
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ISecurity Security { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
             public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                 {
                     options.AddPolicy("AllowAll",
                         builder =>
                         {
                             builder
                             .AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
                         });
                 });
            services.AddMvc(
               config =>
               {
                   config.Filters.Add(typeof(CustomExceptionFilter));
               }
           );
            services.AddOptions();

            ////Register Appsetting---------------------------------------------------------- 
            services.AddSingleton<IConfiguration>(Configuration);
            //services.AddSingleton<ISecurity>(Security);

            //services.AddDbContext<ETSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDatabase")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ////----------Register Interface-------------------------------------------------
            services.AddTransient<ISecurity, SecurityService>();
            #region AuthenticationAddTransient
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = SchemesNamesConst.TokenAuthenticationDefaultScheme;

            })
            .AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(SchemesNamesConst.TokenAuthenticationDefaultScheme, o => { });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = PermissionModuleConst.ModuleAuthenticationDefaultScheme;

            })
            .AddScheme<ModuleAuthenticationOptions, PermissionRequirement>(PermissionModuleConst.ModuleAuthenticationDefaultScheme, o => { });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseEndpointRouting();
            
            string CurrentDirectory = Directory.GetCurrentDirectory();

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseForwardedHeaders();
            app.UseStaticFiles();

            string Resources = "Resources";
            string ResourcesURL = Path.Combine(CurrentDirectory, Resources);
            if (!Directory.Exists(ResourcesURL))
            {
                Directory.CreateDirectory(ResourcesURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(ResourcesURL),
                RequestPath = "/" + Resources
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(ResourcesURL),
                RequestPath = "/" + Resources
            });

            string Images = "Resources/Images";
            string ImagesURL = Path.Combine(CurrentDirectory, Images);
            if (!Directory.Exists(ImagesURL))
            {
                Directory.CreateDirectory(ImagesURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(ImagesURL),
                RequestPath = "/" + Images
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(ImagesURL),
                RequestPath = "/" + Images
            });

            string storeprofileImages = "Resources/StoreProfileImage";
            string StoreprofileURL = Path.Combine(CurrentDirectory, storeprofileImages);
            if (!Directory.Exists(StoreprofileURL))
            {
                Directory.CreateDirectory(StoreprofileURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(StoreprofileURL),
                RequestPath = "/" + storeprofileImages
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(StoreprofileURL),
                RequestPath = "/" + storeprofileImages
            });


            string RaiseClaimProductImage = "RaiseClaimProductImage";
            string RaiseClaimProductImageURL = Path.Combine(CurrentDirectory, RaiseClaimProductImage);
            if (!Directory.Exists(RaiseClaimProductImageURL))
            {
                Directory.CreateDirectory(RaiseClaimProductImageURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(RaiseClaimProductImageURL),
                RequestPath = "/" + RaiseClaimProductImage
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(RaiseClaimProductImageURL),
                RequestPath = "/" + RaiseClaimProductImage
            });



            string TicketAttachment = "TicketAttachment";
            string TicketAttachmentURL = Path.Combine(CurrentDirectory, TicketAttachment);
            if (!Directory.Exists(TicketAttachmentURL))
            {
                Directory.CreateDirectory(TicketAttachmentURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(TicketAttachmentURL),
                RequestPath = "/"+ TicketAttachment
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(TicketAttachmentURL),
                RequestPath = "/"+ TicketAttachment
            });

            string ReportDownload = "ReportDownload";
            string ReportDownloadURL = Path.Combine(CurrentDirectory, ReportDownload);
            if (!Directory.Exists(ReportDownloadURL))
            {
                Directory.CreateDirectory(ReportDownloadURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(ReportDownloadURL),
                RequestPath = "/"+ ReportDownload
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(ReportDownloadURL),
                RequestPath = "/" + ReportDownload
            });


            string BulkUpload = "BulkUpload";
            string BulkUploadURL = Path.Combine(CurrentDirectory, BulkUpload);
            if (!Directory.Exists(BulkUploadURL))
            {
                Directory.CreateDirectory(BulkUploadURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(BulkUploadURL),
                RequestPath = "/" + BulkUpload
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(BulkUploadURL),
                RequestPath = "/" + BulkUpload
            });


            string BulkUploadErrorFilePath = "BulkUpload/Downloadfile/Ticketing/Error";
            string BulkUploadErrorFilePathURL = Path.Combine(CurrentDirectory, BulkUploadErrorFilePath);
            if (!Directory.Exists(BulkUploadErrorFilePathURL))
            {
                Directory.CreateDirectory(BulkUploadErrorFilePathURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(BulkUploadErrorFilePathURL),
                RequestPath = "/" + BulkUploadErrorFilePath
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(BulkUploadErrorFilePathURL),
                RequestPath = "/" + BulkUploadErrorFilePath
            });


            string BulkUploadSuccessFilePath = "BulkUpload/Downloadfile/Ticketing/Success";
            string BulkUploadSuccessFilePathURL = Path.Combine(CurrentDirectory, BulkUploadSuccessFilePath);
            if (!Directory.Exists(BulkUploadSuccessFilePathURL))
            {
                Directory.CreateDirectory(BulkUploadSuccessFilePathURL);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(BulkUploadSuccessFilePathURL),
                RequestPath = "/" + BulkUploadSuccessFilePath
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(BulkUploadSuccessFilePathURL),
                RequestPath = "/" + BulkUploadSuccessFilePath
            });



            app.UseMvc();
        }

        public Authenticate GetAuthenticateDataFromToken(string _radisCacheServerAddress, string _token)
        {
            Authenticate authenticate = new Authenticate();

            try
            {
                RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                if (cacheService.Exists(_token))
                {
                    string _data = cacheService.Get(_token);
                    authenticate = JsonConvert.DeserializeObject<Authenticate>(_data);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return authenticate;
        }

        public string DecryptStringAES(string cipherText)
        {

            var keybytes = Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");
            var iv = Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = Decrypt(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        private string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
    }
}
