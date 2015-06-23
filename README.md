# jaMAL
Just Another MyAnimeList (jaMAL) is a portable C# library for .NET, Windows Phone, and Windows Store to communicate with [MyAnimeList.net](http://myanimelist.net/). It supports query anime and manga from MyAnimeList. Manipulating the anime and manga list of one or more users; adding, removing and updating of the entries. Features a full asynchronous interface, automatic cache of data and an observable design thought for data easy binding.

### Supported Platforms

* .Net (≥ 4.5)
* Windows Phone (≥ 8.1)
* Windows Store (≥ 8)

### Main features

* Work asynchronously or synchronously
* Automatic cache of querys, anime and manga lists and images
* Sync with MyAnimeList when you want
* Observable interface design

## Hello World
You can do everything with asynchronous calls to ensure great responsiveness in your application. But if you just don't care about that you can also do everything synchronously, or mixing asynchronous and synchronous calls. Just do it your way :)

### Synchronous
```C#
Account user = new Account("username", "password");
if( !user.VerifyAccount() )
  throw new Exception("Verification failed :'(");

// get the anime and manga list from myanimelist
user.UserAnimeList.RefreshList();

// show the anime and manga lists of the user
Console.WriteLine(user.ToString());

// get Fullmetal Alchemist anime information
Anime FullmetalAlchemist = MediaDataBase.GetAnime("Fullmetal Alchemist");
Console.WriteLine(FullmetalAlchemist.ToString());

// add Fullmetal Alchemist to the user anime list
AnimeEntry fmaEntry = new AnimeEntry(FullmetalAlchemist, 22/*episode*/, MediaEntry.EntryStatus.Currently);
user.UserAnimeList.AnimeEntries.Add(fmaEntry.Id, fmaEntry);

// add, remove and update all the entries that you wish from the lists :)

// now sync so the changes are send to myanimelist
user.UserAnimeList.SyncAnimeList();
```

### Asynchronous
```C#
Account user = new Account("jaMALTestAccount", "jaMALTestAccount");
if( !user.VerifyAccount() )
    throw new Exception("Verification failed :'(");

IAsyncResult res = null;
// gets the anime and manga list from myanimelist
res = user.UserAnimeList.BeginRefreshList(r =>
    {
        // after we refresh we show the anime and manga lists of the user
        Console.WriteLine(user.ToString());
    });

// now we wait to finish refreshing
res.AsyncWaitHandle.WaitOne();

// get Fullmetal Alchemist anime information
bool finishGettingAnime = false;
Anime FullmetalAlchemist = null;
res = MediaDataBase.BeginGetAnime("Fullmetal Alchemist", r =>
    {
        // after we show the result of the query
        Service.SearchAnimeAsyncResult ra = r as Service.SearchAnimeAsyncResult;
        FullmetalAlchemist = ra.Animes[0];
        Console.WriteLine(FullmetalAlchemist.ToString());
        finishGettingAnime = true;
    });

// now we wait to finish getting the anime and executing the callback
res.AsyncWaitHandle.WaitOne();
while( !finishGettingAnime )
{ }

// add Fullmetal Alchemist to the user anime list
AnimeEntry fmaEntry = new AnimeEntry(FullmetalAlchemist, 22/*episode*/, MediaEntry.EntryStatus.Currently);
user.UserAnimeList.AnimeEntries.Add(fmaEntry.Id, fmaEntry);

// add, remove or update the entries that you want from the lists :)

// now sync so the changes are send to myanimelist
user.UserAnimeList.BeginSyncAnimeList(r =>
    {
        // do some stuff after we sync
    });
```

## Installation
Install the [jaMAL NuGet Package](https://www.nuget.org/packages/jaMAL/).

Too lazy for nuget and to build it yourself? Ok, [here](http://1drv.ms/1LoPHyE) you got a direct download. But don't hate me if that isn’t the last version.

## How to build
To build the project you need:
* Visual Studio ≥ 2013
* Windows Phone ≥ 8.1 SDK
* Windows ≥ 8 (because you cannot install WP8.1 in older versions)

If you have the previous requirements then:

1.  Clone the repository
2.  Excecute getDependencies.bat to download dependencies nuget's packages
3.  Now you are good to go build the projects :)

## Running Tests
To test if everything is working run the tests in the jaMAL-APITest project. Please check that you have internet access before running the tests.

## Reporting bugs
To report bugs, please use the [Issue Tracker](https://github.com/bobxiv/jaMAL/issues)

Steps to report a bug:
* Open the [url](https://github.com/bobxiv/jaMAL/issues/new)
* Add all the needed information to reproduce the bug, the information include
    * library version
    * steps to reproduce the bug
    * some pseudocode

## Contact

* Mail: erojasfredini@gmail.com
* Twitter: [https://twitter.com/erojasfredini](https://twitter.com/erojasfredini)

## License
This software is release under [LGPL](http://www.gnu.org/licenses/lgpl.html).
