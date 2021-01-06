# TODO:


1. Call the Domain directly from a Lambda
2. Get the connection string as secur parameter 

3. Think about deploy this to a lambda which will be called when a new release is deployed
   1. CI: How to deploy a console application to lambda
4. Think about transactional execution for MySQL
5. Roll back

# Scripts Notes
1. Each script should be IdemPotent

# Read:
https://mcode.it/blog/2020-03-15-database_migrations_tips/

# Concerns:

1. Client Specific scripts
    Created a CustomScript Provider class: EmbeddedScriptsProviderByClient
2. Store Store Procedures 
    https://dbup.readthedocs.io/en/latest/more-info/journaling/ -> filter by scripts which starts with `sp_`

    ```
    DeployChanges.To
    .SqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(
        Assembly.GetExecutingAssembly(),
        s => s.Contains("everytime"))
    .JournalTo(new NullJournal())
    .Build();
    ``` 


# Reference creating the DB in Ubuntu with WSL2

https://askubuntu.com/questions/1249973/connect-mysql-workbench-to-wsl2
https://docs.microsoft.com/es-es/windows/wsl/tutorials/wsl-database
wsl hostname -I
netsh interface portproxy add v4tov4 listenport=19000 listenaddress=0.0.0.0 connectport=3306 connectaddress=$($(wsl hostname -I).Trim());

