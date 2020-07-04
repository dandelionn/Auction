### Tema de laborator

* Sa se creeze o aplicatie in care se permite licitarea pentru produse vandute de catre utilizatori. Produsele sunt caracterizate prin apartenenta la categorii, acestea putand face parte din alte categorii etc. (ex: electronice <- aparatura foto <- aparat foto concret). __Se va modela situatia in care o categorie poate sa aiba mai multi parinti – conditie de luare in considerare a temei.__
&nbsp;
* Se cere implementare care sa trateze urmatoarele:
1.  - Pentru un produs se specifica data de incepere si data de finalizare a licitatiei
    - Se va valida ca data inceperii < data finalizarii, __dar diferente sa fie de maxim 4 luni de zile__, sau ca data inceperii nu poate sa fie in trecut (relativ la momentul prezent)
    - Impuneti alte validari ale valorilor utilizate.
&nbsp;
2.  - Pentru fiecare produs pentru care se incepe licitatia se specifica un pret de pornire   
    - Se vor rejecta preturi de pornire mai mici decat un prag, predefinit ca o constanta a aplicatiei 
    - La inceputul licitatiei se va specifica o moneda de tranzactie si toate celelalte licitari pentru obiectul curent trebuie sa se faca in aceasta moneda __(validati sau modelati corespunzator)__
    - Pentru o licitare, pretul oferit la momentul n+1 trebuie sa fie strict mai mare decat pretul de la momentul n, __dar cel mult cu 10% mai mult fata de pretul de la momentul n__.
    - Impuneti alte validari asupra datelor, demonstrati prin testare automata implementarea.
&nbsp;
3.  - Finalizarea unui proces de licitatie se face:
        - fie prin atingerea datei declarate pentru finalizare
        - fie prin incheierea la dorinta de catre cel care a depus obiectul la licitare (o persoana nu poate sa incheie o licitatie pe care nu a demarat-o);
    - Pentru o licitatie incheiata nu se mai poate modifica nimic. 
    - Impuneti validari asupra fluxului de operatii si acoperiti cu teste.
&nbsp;
4.  - O persoana care depune obiecte spre a fi licitate nu poate sa aiba la un moment dat mai mult de k licitatii incepute si nefinalizate (finalizare = incheierea perioadei de finalizare sau terminarea explicita de catre ofertantul obiectului).
    - Valoarea k este constanta a aplicatiei ce se va citi din baza de date sau din fisier de configurare. Modificarea ei nu trebuie sa duca la recompilarea si re-deploy de aplicatie.
&nbsp;
5.  - Nu se permite ca o persoana sa aiba la un moment dat mai mult de m licitatii demarate si nefinalizate in cadrul unei categorii. 
    - Se permite deschiderea unei alte licitatii in acea categorie doar momentul in care numarul celor active (deschise de utilizator) este sub pragul m.
&nbsp;
6.  - Pentru fiecare persoana care foloseste platforma se asigneaza un scor, reprezentand seriozitatea 
    - Scorul este cu o valoare initiala S, pana la prima asignare de scor facuta de alt utilizator, moment dupa care se folosesc doar scorurile date de utilizatori
    - Pentru cel putin un scor asociat unui utilizator, scorul total este media ultimelor cel mult n scoruri (n citit din fisier de configurare sau baza de date, n >= 1). 
    - Daca scorul unei persoane scade sub un prag prestabilit p, atunci persoana va fi impiedicata sa deschida noi licitatii timp de z zile (p si z preluate din fisier de configurare sau baza de date).

* Roluri (neexcluzive mutual, se preiau la crearea contului sau ulterior):
    1. ofertant de obiect
    2. licitant

* Cerinte:
    1. Unit testing folosind NUnit, xUnit sau Visual Studio 2017+; minim 210 de metode de testare (__conditie de luare in considerare a temei__); se vor testa daca valorile proprietatilor au valori adecvate, restrictiile de mai sus si altele de validare a corectitudinii rezultatelor sia fluxului de lucru.
    &nbsp;
    2. Folosire de blocuri din suita Microsoft Enterprise Library sau echivalente: logging (se poate folosi alternativ log4net), validation application block (sau se poate scrie cod de validare manual, sau se poate folosi o biblioteca de tipul https://github.com/JeremySkinner/FluentValidation), optional: exception handling application block, optional: security application block. Nu se face logging in unit testing (se permite in straturile aplicatiei, insa). __Validarea starii obiectelor prin teste este conditie obligatorie pentru luarea in considere a temei. Logging-ul este obligatoriu pentru luarea in considerare a temei.__
    &nbsp;
    3. Dezvoltare pe straturi (layers): Domain layer si Data Layer __(conditie de luare in considerare a temei)__; nu se ia in considerare/nu se cere user interface. Se pot folosi alte pattern-uri de design, daca se considera necesar (DTO, remote facade etc.).
    &nbsp;
    4. Baza de date SQL Server 2014+, MySQL, Oracle sau PostgreSQL.
    &nbsp;
    5. Domain Layer implementat prin Domain Model; Data Layer implementat prin Data Mapper(__conditiile de la acest punct sunt obligatorii__).
    &nbsp;
    6. Code coverage de minim 90% pentru codul de Domain Model si minim 90% pentru Service
    Layer (conditie de luare in considerare a temei).
    &nbsp;
    7. Mocking (__conditie de luare in considerare a temei__).
    &nbsp;
    8. Comentarii, minim pentru metode, constructori, proprietati, indexatori, destructori, tipuri imbricate. Puteti folosi GhostDoc.
    &nbsp;
    9. 
        - Se va face analiza codului sursa folosind StyleCop (plugin de Visual Studio; versiune recomandata 4.7.x). 
        -   Toate regulile (tab-ul Rules) trebuie bifate; in tab-ul Company Information la sectiunea „Company Name” sa figureze „Transilvania University of Brasov”, iar la Copyright numele studentului. 
        - Maxim 30 de avertismente sunt permise (__conditiile de la acest punct sunt obligatorii__). 
        - Puteti folosi pluginuri auxiliare de forma CodeItRight.Notarea aplicatiei se va face prin rularea unitatilor de test scrise. 
        - Unitatile de test vor include validarea starii obiectelor (poate aparea validare selectiva, in anumite contexte doar anumite proprietati trebuie sa fie satisfacute) si verificarea coerentei la nivel de service layer (daca parametrii trimisi unor metode de serviciu nu sunt legali, e de asteptat ca metoda sa arunce exceptie sau sa returneze un rezultat din care se deduce faptul ca nu se poate indeplini cererea); validati fluxul de mesaje, starea curenta si contextual; urmariti cerintele si precizarile din enuntul temei. 
    &nbsp;
    
* Studentii se pot consulta in legatura cu tema, dar munca va fi individuala. Implementarile pentru care exista suspiciune de frauda vor fi notate cu 1.