//#define AUTH
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using Xamarin.Forms;

using System.Diagnostics;
using Jammer_1.Helpers;
using Jammer_1.Models;
using Jammer_1.Services;

using Newtonsoft.Json.Linq;
using System.Threading;

[assembly: Dependency(typeof(AzureService))]
namespace Jammer_1.Services
{
    public partial class AzureService
    {
        public static AzureService defaultInstance = new AzureService();
        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<Models.User> user_table;
        IMobileServiceSyncTable<Models.About_user> about_user_table;
        IMobileServiceSyncTable<Models.Instrument> instrument_table;
        IMobileServiceSyncTable<Models.Jammer_rating> jammer_rating_table;
        IMobileServiceSyncTable<Models.User_match_setttings> user_match_settings_table;
        IMobileServiceSyncTable<Models.Jam> jam_table;
        IMobileServiceSyncTable<Models.Availability> availability_table;
        IMobileServiceSyncTable<Models.Jam_location> jam_location_table;
        IMobileServiceSyncTable<Models.Jam_preferences> jam_preferences_table;



        public static AzureService DefaultManager
        {
            
            get
            {
                defaultInstance.createserviceclient();
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }
        public MobileServiceClient CurrentClient
        {
            get { return Client; }
        }
        public void createserviceclient()
        {
            this.Client = new MobileServiceClient(
                Constants.ApplicationURL);
         ///  Initialize("user_table");
        }
            /// <summary>
            /// This is initializes a json file for the database_table you want. All you have to do is give it a a table name
            /// </summary>
            /// <param name="DatabaseName"> name of table </param>
            /// <returns></returns>
            public async Task Initialize(string DatabaseName)
        {
        if (Client?.SyncContext?.IsInitialized ?? false)
             return;

                //Create our client

                Client = new MobileServiceClient(Constants.ApplicationURL);

            //InitialzeDatabase for path
            var path = DatabaseName + ".db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            if (DatabaseName.Equals("user_table"))
            {
                store.DefineTable<User>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                user_table = Client.GetSyncTable<User>();
            }
            if (DatabaseName.Equals("about_user_table"))
            {
                store.DefineTable<Models.About_user>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                about_user_table = Client.GetSyncTable<Models.About_user>();
            }
            if (DatabaseName.Equals("instrument_table"))
            {
                store.DefineTable<Models.Instrument>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                instrument_table = Client.GetSyncTable<Models.Instrument>();
            }
   
            if (DatabaseName.Equals("jammer_rating_table"))
            {
                store.DefineTable<Models.Jammer_rating>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                jammer_rating_table = Client.GetSyncTable<Models.Jammer_rating>();
            }

            if (DatabaseName.Equals("user_match_settings_table"))
            {
                store.DefineTable<Models.User_match_setttings>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                user_match_settings_table = Client.GetSyncTable<Models.User_match_setttings>();
            }
            if (DatabaseName.Equals("jam_table"))
            {
                store.DefineTable<Models.Jam>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                jam_table = Client.GetSyncTable<Models.Jam>();
            }
            if (DatabaseName.Equals("availability_table"))
            {
                store.DefineTable<Models.Availability>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                availability_table = Client.GetSyncTable<Models.Availability>();
            }
            if (DatabaseName.Equals("jam_location_table"))
            {
                store.DefineTable<Models.Jam_location>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                jam_location_table = Client.GetSyncTable<Models.Jam_location>();
            }
            if (DatabaseName.Equals("jam_preferences_table"))
            {
                store.DefineTable<Models.Jam_preferences>();
                //Initialize SyncContext
                await Client.SyncContext.InitializeAsync(store);

                //Get our sync table that will call out to azure
                jam_preferences_table = Client.GetSyncTable<Models.Jam_preferences>();
            }

        }
       /// <summary>
        /// Sync the correct table. This function needs a database name and  because that is how we build
        /// the final database name(+ _databasename)
        /// </summary>
        /// <param name="DatabaseName"> name of table </param>
        /// <param name="">  the user is ins </param>
        /// <returns></returns>

        public async Task SyncTable(string DatabaseName)
        {
           
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                if (DatabaseName.Equals("user_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await user_table.PullAsync("allUser", user_table.CreateQuery());

                    
                    
                }
                if (DatabaseName.Equals("about_user_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await about_user_table.PullAsync("allAbout_user", about_user_table.CreateQuery());

                  
                }
                if (DatabaseName.Equals("instrument_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await instrument_table.PullAsync("allInstrument", instrument_table.CreateQuery());

                   
                }

                if (DatabaseName.Equals("jammer_rating_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await jammer_rating_table.PullAsync("allJammer_rating", jammer_rating_table.CreateQuery());

                   
                }

                if (DatabaseName.Equals("user_match_settings_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await user_match_settings_table.PullAsync("allUser_match_settings", user_match_settings_table.CreateQuery());

                   

                }
                if (DatabaseName.Equals("jam_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await jam_table.PullAsync("allJam", jam_table.CreateQuery());
                    

                }
                if (DatabaseName.Equals("availability_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await availability_table.PullAsync("allAvailablity", availability_table.CreateQuery());
                    

                }
                if (DatabaseName.Equals("jam_location_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await jam_location_table.PullAsync("allJam_location", jam_location_table.CreateQuery());
                    

                }
                if (DatabaseName.Equals("jam_preferences_table"))
                {
                    await Client.SyncContext.PushAsync();
                    await jam_preferences_table.PullAsync("alljam_preferences", jam_preferences_table.CreateQuery());
                    

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync to tables, that is alright as we have offline capabilities: " + ex);
            }

        }
        /// <summary>
        ///Initialize & Sync
        /// </summary>
        /// <param name="DatabaseName"> name of table </param>
        /// <param name="">  the user is ins </param>
        /// <returns></returns>

        public async Task<IEnumerable<Object>> GetTable(string DatabaseName)
        {

            //fucntion will return user table if DatabaseName is blank

            //create an empty table;
            await Initialize(DatabaseName);
            await SyncTable(DatabaseName);


            if (DatabaseName.Equals("about_user_table"))
            {
                ///order by Id
                return await about_user_table.OrderBy(c => c.Id).ToEnumerableAsync(); ;
            }

            if (DatabaseName.Equals("instrument_table"))
            {
                //ordr by id
                return await instrument_table.OrderBy(c => c.Id).ToEnumerableAsync(); ;
            }
            if (DatabaseName.Equals("jammer_rating_table"))
            {
                //order by id
                return await jammer_rating_table.OrderBy(c => c.Id).ToEnumerableAsync(); ;
            }

            if (DatabaseName.Equals("user_match_settings_table"))
            {
                //order by id
                return await user_match_settings_table.OrderBy(c => c.Id).ToEnumerableAsync(); ;

            }
            if (DatabaseName.Equals("jam_table"))
            {
                //order by id
                return await jam_table.OrderBy(c => c.Id).ToEnumerableAsync(); ;

            }
            if (DatabaseName.Equals("availability_table"))
            {
                //order by id
                return await availability_table.OrderBy(c => c.User_id).ToEnumerableAsync(); ;

            }
            if (DatabaseName.Equals("jam_location_table"))
            {
                //order by id
                return await jam_location_table.OrderBy(c => c.Id).ToEnumerableAsync(); ;

            }
            if (DatabaseName.Equals("jam_preferences_table"))
            {
                //order by id
                return await jam_preferences_table.OrderBy(c => c.Id).ToEnumerableAsync(); ;

            }
            return await user_table.OrderBy(c => c.Id).ToEnumerableAsync();

        }
        /// <summary>
        /// Add an item to desired table. 
        /// Uses Item and  again because it calls sync which uses 
        /// </summary>
        /// <param name="Item"> Model that you are looking to delete</param>
        /// <param name="">  the user is ins </param>
        /// <returns></returns>

        public async Task<bool> Add_item_to_table(Object Item)
        {

            if (Item is User)
            {
                await Initialize("user_table");
                Models.User user = (Models.User)Item;
                await user_table.InsertAsync(user);
                await SyncTable("user_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.About_user)
            {
                await Initialize("about_user_table");
                Models.About_user About_user = (Models.About_user)Item;
                await about_user_table.InsertAsync(About_user);
                await SyncTable("about_user_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.Instrument)
            {
                await Initialize("instrument_table");
                Models.Instrument Instrument = (Models.Instrument)Item;
                await instrument_table.InsertAsync(Instrument);
                await SyncTable("instrument_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.Jammer_rating)
            {
                await Initialize("jammer_rating_table");
                Models.Jammer_rating Jammer_rating = (Models.Jammer_rating)Item;
                await jammer_rating_table.InsertAsync(Jammer_rating);
                await SyncTable("about_user_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.User_match_setttings)
            {
                await Initialize("user_match_settings_table");
                Models.User_match_setttings User_match_settings = (Models.User_match_setttings)Item;
                await user_match_settings_table.InsertAsync(User_match_settings);
                await SyncTable("user_match_settings_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.Jam)
            {
                await Initialize("jam_table");
                Models.Jam Jam = (Models.Jam)Item;
                await jam_table.InsertAsync(Jam);
                await SyncTable("jam_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.Availability)
            {
                await Initialize("availability_table");
                Models.Availability Availability = (Models.Availability)Item;
                await availability_table.InsertAsync(Availability);
                await SyncTable("availability_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.Jam_location)
            {
                await Initialize("jam_location_table");
                Models.Jam_location Jam_Location = (Models.Jam_location)Item;
                await jam_location_table.InsertAsync(Jam_Location);
                await SyncTable("jam_location_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.Jam_preferences)
            {
                await Initialize("jam_preferences_table");
                Models.Jam_preferences Jam_Preferences = (Models.Jam_preferences)Item;
                await jam_preferences_table.InsertAsync(Jam_Preferences);
                await SyncTable("jam_preferences_table");
                return await Task.FromResult(true);
            }




            return await Task.FromResult(false);
        }
        /// <summary>
        /// Deletes Item for table
        ///  Uses Item and  again because it calls sync which uses 
        /// </summary>
        /// <param name="Item"> Model that you are looking to delete</param>
        /// <param name="">  the user is ins </param>
        /// <returns></returns>

        public async Task<bool> Delete_Item_from_table(Object Item)
        {
            Object returnthis = new object();

            if (Item is Models.User)
            {
                await Initialize("user_table");
                Models.User user = (Models.User)Item;
                await user_table.DeleteAsync(user);
                await SyncTable("user_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.About_user)
            {
                await Initialize("about_user_table");
                Models.About_user About_user = (Models.About_user)Item;
                await about_user_table.DeleteAsync(About_user);
                await SyncTable("about_user_table");
                return await Task.FromResult(true);


            }
            if (Item is Models.Instrument)
            {
                await Initialize("instrument_table");
                Models.Instrument Instrument = (Models.Instrument)Item;
                await instrument_table.DeleteAsync(Instrument);
                await SyncTable("instrument_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.Jammer_rating)
            {
                await Initialize("jammer_rating_table");
                Models.Jammer_rating Jammer_rating = (Models.Jammer_rating)Item;
                await jammer_rating_table.DeleteAsync(Jammer_rating);
                await SyncTable("jammer_rating_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.User_match_setttings)
            {
                await Initialize("user_match_settings_table");
                Models.User_match_setttings User_match_settings = (Models.User_match_setttings)Item;
                await user_match_settings_table.DeleteAsync(User_match_settings);
                await SyncTable("user_match_settings_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.Availability)
            {
                await Initialize("availability_table");
                Models.Availability Availablity = (Models.Availability)Item;
                await availability_table.DeleteAsync(Availablity);
                await SyncTable("availability_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.Jam_location)
            {
                await Initialize("jam_location_table");
                Models.Jam_location Jam_Location = (Models.Jam_location)Item;
                await jam_location_table.DeleteAsync(Jam_Location);
                await SyncTable("jam_location_table");
            }
            if (Item is Models.Jam_preferences)
            {
                await Initialize("jam_preferences_table");
                Models.Jam_preferences Jam_Preferences = (Models.Jam_preferences)Item;
                await jam_preferences_table.DeleteAsync(Jam_Preferences);
                await SyncTable("jam_preferences_table");
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        /// <summary>
        /// update item in table
        /// </summary>
        /// <param name="Item"> Model that you are looking to delete</param>
        /// <param name="">  the user is ins </param>
        /// <returns></returns>
        public async Task<bool> Update_item_in_table(Object Item)
        {
            if (Item is Models.User)
            {
                await Initialize("user_table");
                Models.User user = (Models.User)Item;
                await user_table.UpdateAsync(user);
                await SyncTable("user_table");
                return await Task.FromResult(true);
            }
            if (Item is Models.About_user)
            {
                await Initialize("about_user_table");
                Models.About_user About_user = (Models.About_user)Item;
                await about_user_table.UpdateAsync(About_user);
                try
                {
                    await Client.SyncContext.PushAsync();
                  ///  await about_user_table.PullAsync("all About_user", about_user_table.CreateQuery());
                }
               catch (MobileServicePushFailedException ex)
               {
                    if (ex.PushResult != null)
                   {
                       foreach (var error in ex.PushResult.Errors)
                       {
                          await ResolveConflictAsync(error,"about_user_table");
                       }
                    }
                }
                ///  await SyncTable("about_user_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.Instrument)
            {
                await Initialize("instrument_table");
                Models.Instrument Instrument = (Models.Instrument)Item;
                await instrument_table.UpdateAsync(Instrument);
                try
                {
                    await Client.SyncContext.PushAsync();
                    ///  await about_user_table.PullAsync("all About_user", about_user_table.CreateQuery());
                }
                catch (MobileServicePushFailedException ex)
                {
                    if (ex.PushResult != null)
                    {
                        foreach (var error in ex.PushResult.Errors)
                        {
                            await ResolveConflictAsync(error, "instrument_table");
                        }
                    }
                }
                return await Task.FromResult(true);

            }
            if (Item is Models.Jammer_rating)
            {
                await Initialize("jammer_rating_table");
                Models.Jammer_rating Jammer_rating = (Models.Jammer_rating)Item;
                await jammer_rating_table.UpdateAsync(Jammer_rating);
                await SyncTable("jammer_rating_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.User_match_setttings)
            {
                await Initialize("user_match_settings_table");
                Models.User_match_setttings User_match_settings = (Models.User_match_setttings)Item;
                await user_match_settings_table.UpdateAsync(User_match_settings);
                await SyncTable("user_match_settings_table");
                try
                {
                    await Client.SyncContext.PushAsync();
                    ///  await about_user_table.PullAsync("all About_user", about_user_table.CreateQuery());
                }
                catch (MobileServicePushFailedException ex)
                {
                    if (ex.PushResult != null)
                    {
                        foreach (var error in ex.PushResult.Errors)
                        {
                            await ResolveConflictAsync(error, "user_match_settings_table");
                        }
                    }
                }
                return await Task.FromResult(true);

            }
            if (Item is Models.Jam)
            {
                await Initialize("jam_table");
                Models.Jam Jam = (Models.Jam)Item;
                await jam_table.UpdateAsync(Jam);
                await SyncTable("jam_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.Availability)
            {
                await Initialize("availability_table");
                Models.Availability Availablity = (Models.Availability)Item;
                await availability_table.UpdateAsync(Availablity);
                await SyncTable("availability_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.Jam_location)
            {
                await Initialize("jam_location_table");
                Models.Jam_location Jam_Location = (Models.Jam_location)Item;
                await jam_location_table.UpdateAsync(Jam_Location);
                await SyncTable("jam_location_table");
                return await Task.FromResult(true);

            }
            if (Item is Models.Jam_preferences)
            {
                await Initialize("jam_preferences_table");
                Models.Jam_preferences Jam_Preferences = (Models.Jam_preferences)Item;
                await jam_preferences_table.UpdateAsync(Jam_Preferences);
                await SyncTable("jam_preferences_table");
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        async Task ResolveConflictAsync(MobileServiceTableOperationError error, string table_name)
        {

            if (table_name.Equals("about_user_table")) {
               var  serverItem= error.Result.ToObject<About_user>();
               var localItem = error.Item.ToObject<About_user>();
       
            

            // Note that you need to implement the public override Equals(TodoItem item)
            // method in the Model for this to work
            if (serverItem.Equals(localItem))
            {
                // Items are the same, so ignore the conflict
                await error.CancelAndDiscardItemAsync();
                return;
            }

            // Client Always Wins
            localItem.Version = serverItem.Version;
            await error.UpdateOperationAsync(JObject.FromObject(localItem));
            }
            // Server Always Wins
            // await error.CancelAndDiscardItemAsync();
            if (table_name.Equals("instrument_table"))
            {
                var serverItem = error.Result.ToObject<Instrument>();
                var localItem = error.Item.ToObject<Instrument>();



                // Note that you need to implement the public override Equals(TodoItem item)
                // method in the Model for this to work
                if (serverItem.Equals(localItem))
                {
                    // Items are the same, so ignore the conflict
                    await error.CancelAndDiscardItemAsync();
                    return;
                }

                // Client Always Wins
                localItem.Version = serverItem.Version;
                await error.UpdateOperationAsync(JObject.FromObject(localItem));
            }
            // Server Always Wins
            // await error.CancelAndDiscardItemAsync();
            if (table_name.Equals("user_match_settings_table"))
            {
                var serverItem = error.Result.ToObject<User_match_setttings>();
                var localItem = error.Item.ToObject<User_match_setttings>();



                // Note that you need to implement the public override Equals(TodoItem item)
                // method in the Model for this to work
                if (serverItem.Equals(localItem))
                {
                    // Items are the same, so ignore the conflict
                    await error.CancelAndDiscardItemAsync();
                    return;
                }

                // Client Always Wins
                localItem.Version = serverItem.Version;
                await error.UpdateOperationAsync(JObject.FromObject(localItem));
            }
            // Server Always Wins
            // await error.CancelAndDiscardItemAsync();
        }

        public async Task purgetable(string DatabaseName)
        {

            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                if (DatabaseName.Equals("user_table"))
                {
 
                    await user_table.PurgeAsync(null, null, true, CancellationToken.None);



                }
                if (DatabaseName.Equals("about_user_table"))
                {

                    await about_user_table.PurgeAsync(null, null, true, CancellationToken.None);


                }
                if (DatabaseName.Equals("instrument_table"))
                {

                    await instrument_table.PurgeAsync(null, null, true, CancellationToken.None);


                }

                if (DatabaseName.Equals("jammer_rating_table"))
                {

                    await jammer_rating_table.PurgeAsync(null, null, true, CancellationToken.None);


                }

                if (DatabaseName.Equals("user_match_settings_table"))
                {

                    await user_match_settings_table.PurgeAsync(null, null, true, CancellationToken.None);



                }
                if (DatabaseName.Equals("jam_table"))
                {

                    await jam_table.PurgeAsync(null, null, true, CancellationToken.None);


                }
                if (DatabaseName.Equals("availability_table"))
                {

                    await availability_table.PurgeAsync(null, null, true, CancellationToken.None);

                }
                if (DatabaseName.Equals("jam_location_table"))
                {
                    await jam_location_table.PurgeAsync(null, null, true, CancellationToken.None);


                }
                if (DatabaseName.Equals("jam_preferences_table"))
                {
                    await jam_preferences_table.PurgeAsync(null, null, true, CancellationToken.None);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync to tables, that is alright as we have offline capabilities: " + ex);
            }

        }
    }
}
