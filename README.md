# csharp-generation-azure
C# learning project for Generation's Azure Developer Associate course

## Known Issues / Errors

### "Could not find a part of the path ... bin\roslyn\csc.exe" Error

If you see a message saying _**"Could not find a part of the path ... bin\roslyn\csc.exe"**_, please follow these steps:

1.  Open the `Package Manager Console` from **Tools > NuGet Package Manager > Package Manager Console** and type the following command in the opened console:
    ```
    Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
    ```

2.  If you are working with this repository, open a terminal window in the root directory of the cloned project repository and run the following command to get rid of unnecessary file modifications made by the previous command:
    ```
    git checkout .
    ```

3.  If after the second step a pop-up window titled **File Modification Detected** appears, please click on `Reload All` to get the project synchronized again.

**Note**: If this or other errors still appear, you can replace the first step command with `Update-Package -r` to reinstall **ALL** NuGet packages, re-download the project, remove downloaded NuGet packages, delete the created database, delete created configuration or cache files, or even repair the Visual Studio installation.

### "<migration_file>.mdf" failed with the operating system error 2(The system cannot find the file specified.)

If it's impossible to `migrate-database` because of this error, please follow these steps:

1.  Remove `App_Data` folder in **Solution Explorer** if exists. Confirm in window pop-up.

2.  Right-clic in your project in **Solution Explorer**, then clic on **Add > Add ASP.NET Folder > App_Data** to ensure the correct folder exists for this application and it's linked correctly.

Now you should be able to `migrate-database` and execute ASP.NET app without any problem.

### "Cannot attach the file as database"

This error appears because your computer still has a setting configured for the database in the original project. You need to delete that setting.

To solve this problem, please follow these steps:

1.  Open the `Package Manager Console` from **Tools > NuGet Package Manager > Package Manager Console**.

2.  Run the following command to stop the actual `MSSQLLocalDB` instance:
    ```
    sqllocaldb stop MSSQLLocalDB
    ```

3.  Finally, run the following command to delete de current `MSSQLLocalDB` instance:
    ```
    sqllocaldb delete MSSQLLocalDB
    ```

Then, you can run the app again, and the setting will be re-created for the new/copied project.

**Note**: If it still failing, please run `sqllocaldb info` and delete the rest of reported instances repeating steps 2. and 3. (do not delete those instances you recognize of your other projects).

### Unable to update database to match the current model because there are pending changes and automatic migration is disabled...

...Either write the pending model changes to a code-based migration or enable automatic migration. Set DbMigrationsConfiguration.AutomaticMigrationsEnabled to true to enable automatic migration. You can use the Add-Migration command to write the pending model changes to a code-based migration.

1.  Please first follow the steps described in the **"<migration_file>.mdf" failed with the operating system error 2(The system cannot find the file specified.)** error to delete the `App_Data` folder.

2.  Then follow the steps in **"Cannot attach the file as database"**.

Now, you should be able to migrate the database without any problems.

### Problem when adding SSL self-signed certificate from IIS Express via Visual Studio prompt

This happens mainly when migrating/upgrading Visual Studio (e.g: from 2017 to 2019) and the IDE prompts you to trust the IIS self-signed certificate (again):

```
Adding the certificate to the Trusted Root Certificates store failed with the following error: Access is denied.
```

To fix this error, it's mandatory to follow the following steps:

1.  Delete _localhost_ from Trusted (System) Root Certificate store under Personal folder.

2.  Repair IIS Express (via Control Panel (Settings app does not allow repairs) -> Programs -> Programs and Features -> IIS Express -> Repair).
    
    **NOTE**: this may erase all your local IIS databases, so back it up if needed.

3.  Restart Visual Studio, run your app (in Debug mode), and let the IDE handle the certificate trusting process (reply "Yes" when prompted to trust it).

If you delete the self-signed certificate but don't repair the IIS Express installation to force a new self-signed certification to be created, you may face errors like `ERR_CONNECTION_RESET`. To fix that, just follow the instructions step by step.

### Problem when using HTTPS on localhost ASP.NET Web App (certificate not recognised by your web browser)

This is a bit strange because I think is related to the differences between Visual Studio and .NET CLI when adding the self-signed .NET certificate (it may have been necessary to restart the browser, but I couldn't do it and the following commands worked flawlessly):

Open a terminal as a system administrator and run the following commands:

```cmd
dotnet dev-certs https –-clean
dotnet dev-certs https –-trust
```

This will firstly delete the current self-signed certificate under the Personal Root Certificate store and then generate a new one.

Once done that, open Visual Studio and run your ASP.NET web app.
