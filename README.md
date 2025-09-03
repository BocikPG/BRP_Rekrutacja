Zadanie rekrutacyjne dla Black Rose Projects

Dokładne instrukcje zadania dostępne na UI sceny `PlayScene`

## Sterowanie

Otwieranie ekwipunku jest pod E (na klawiaturze) lub na górnym przycisku pada (Y na Xbox'ie, trójkąt na PS). 

Otweiranie menu ESC, lub Select na padzie.

Cofanie standardowo pod ESC/B/kółku.

Ale są też ikonki (ale tylko na klawę i xbox'a :P ) żeby łatwiej się połapać.

## Import

Zaimportowałem DOTween do animacji zdobywania punktów.

Ikonki wzięte z Itch'a, CC0 ale w md'u jest link do paczki.

## Kod

Oryginalnego kodu starałem się ruszać jak najmniej, tylko dokładać do niego swoje klocki.

W chyba dwóch miejscach pokusiłem się o mały refaktor, aby nie trzeba było zmieniać kilka razy tego samego.

Dodałem też kilka list, żeby mieć dostęp do nich, bez potrzeby szukania po scenie. Singlety w kontrollerach też dodałem.

Całość ogólnie była czytelna, ale niektóre elementy, jak dodawanie lisenerów do buttonów zrobił bym inaczej (: .

## Wiadome niedociagnięcia

Nie ogarnąłem scrolla pod przeglądarkę dusz. Tak samo, nie rusza się, gdy wybrany element jest poza widocznością (próbowałem, nie zdążyłem).

Wydaje mi się, że czasem w przeglądrce jak ruszę w lewo to select znika, ale nie potrafię tego zreplikować (zazwyczaj działa tho).

## Co można by zrobić lepiej?

Zaprogramować "pamięć", żeby po cofnięciu do parent okienka, być gdzie się było przed wejściem, a nie na pozycji 1.

Więcej animacji do score'a (żeby rósł np.) i wariany przy większym wyniku.

## Z czego jestem zadowolony?

Że można spamować atakami.

Że po wybraniu duszy wybór skacze na użyj i potem na tak w PopUp'ie (czyli że można spamować używanie :P ).

## TLDR

Fun project, would do it again.

