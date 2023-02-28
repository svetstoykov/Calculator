# Calculator
A simple console calculator app built with .NET 7

# Overview

* The app consists of a **Web, Application and Infrastructure** layer. Using the onion architecture, the direction of coupling is from the Web to the Application layer. Some of the Infastructure functionality is used in the App layer via interfaces (i.e. CacheService), this way we abstract away the persistence implementation.

* The foldering and structure used is **"Folder-by-Feature"**. This means that for the different features of the app, we have different folders named **"{Feature}"** and in the nested folders we keep the different components. This means that there will be, for example, a Controller folder inside each feature. The main goal behind this "slice" structure is that we can easily move any of the "features" in a microservice later on. At the moment we have only one feature **"Expressions"** which allows for the evaluation of expressions, however in the future we can add others as well (Graphs, Geometry etc.)
