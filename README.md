# SQL_Contact_Wallet_V3_Final
Program that manages a contact wallet with SQL Server 2017 in XtraWinForms mode with DevExpress

This third version is in XtraWinForms mode thanks to the DevExpress library, when you open the program you can see a gridview that 
contains all your societies and inside the contacts of each society and then you can :

-see the full card of a society after clicking on it and edit it, the card of a society also contains its contacts and the infos for each 
contact

-add a new society, contact or infoContact

Each contact needs to be linked to a society, but you can create a society without any contact working in it

The database contains 3 tables :

InfoContact : -[PK] id -typeInfo -info -[FK] id of the contact

Contact : -[PK] id -[FK] id of the society -title -surname -name -function (can be NULL)

Society : -[PK] id -name of the society -address -second address (can be NULL) -postal code -city -standard (can be NULL)

All SQL and database settings are in the Data/AnnuaireDataSet.x* files

Warning : You need to have DevExpress library 15.1 or greater to build this project

I have only produced the code in the Source folder, files in the Designer folder are automatically generated by Visual Studio
