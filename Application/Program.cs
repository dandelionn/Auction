
// link for configuring log4net
// https://stackify.com/log4net-guide-dotnet-logging/
// ColoredConsoleAppender
// https://stackoverflow.com/questions/36215851/log4net-coloredconsoleappender-not-showing-any-color

/*
 * Enterprise Library
Validation
Validatoare pentru proprietati

Self Validation
Ce inseamna Key, Tag

Adnotarea tipurilor de date pentru a specifica validarea

Using declansare automat inchidetea conexiunii si a readerului

NSK o aplicatie facuta ca lumea
Studiaza proiectul

Validarile se fac cu adnotari
Doar cei din anul 2 folosesc ifuri
*/

// Code coverage alternatives
/*
 * dotCover:
 * OpenCover: https://www.nuget.org/packages/OpenCover/
 * AxoCover: https://github.com/axodox/AxoCover
 * 
 * 
 * Sasu:

-link to object, link to entity framework
-cream baza de date porning de la clase
-Entity framework lazy loading by using virtual keyword
-facem adnotarile corect pe clase si se genereaza o baza de date ca la carte

 
-get insert update delete pt partea de repository
-cheie primara object
-orderBy cand se aduc datele neaparat
-https://fluentvalidation.net/

Factory Method
-https://refactoring.guru/design-patterns/factory-method

Abstract Factory Method
-https://refactoring.guru/design-patterns/abstract-factory


DAL cadou e gratis, il putem folosi. laborator 6

Validare bazata pe adnotari de entity framework,
folosim ceea ce vrem noi.

Fara mocking vei prezenta degeaba
Tot ce e cu rosu e obligatoriu, altfel nu venim
UI nu e necesar, vrea doar de la ServiceLayer
Toata testarea se face in mod automat.
Ne apucam de lucru.

Folosim REPOSITORY PATTERN pt ca accelereaza mult scrierea de cod.
Sunt putini studenti care folosesc acest pattern(Sasu nu stie de ce)

clase generice, interfete generice


LOGING IN BAZA DE DATE(BONUS POINTS)
"Nimeni normal la cap intr-o aplicatie nu face loggingul in fisier, ci il face in baza de date"

NCOVER(licenta de student) + NUNIT
.NET Framework
 */

using DataMapper.SqlServerDAO;
using DomainModel;
using ServiceLayer.ServiceImplementation;
using System;

namespace Application
{
    class Program
    {
        static void Main()
        {
            var userProfile = new UserProfile
            {
                Username = "dandelionn",
                Password = "Password0&",
                Email = "dan@email.com",
            };

            var userProfileServices = new UserProfileServices(new UserProfileRepository());
            userProfileServices.Insert(userProfile);
            var list = userProfileServices.GetAll();
            foreach(var item in list)
            {
                Console.WriteLine(item.Id.ToString(), item.Username);
            }
        }
    }
}
