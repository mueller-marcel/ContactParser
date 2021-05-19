# ContactParser

## Getting Started
* Laden Sie die "ContactParser.exe" vom neusten Release herunter. Sie kann auf dem lokalen Rechner ohne Weiteres ausgeführt werden.
* Die User Stories mit den Akzeptanzkriterien und die DoD finden Sie unten

## Systemvoraussetzungen
Betriebssystem: Windows 7 (64 Bit) oder höher

## Nutzung
### Parsen eines Namens
Geben Sie den Namen in das Eingabefeld ein. Achten Sie darauf, dass die Namenselemente (z.B. Vorname, Nachname, Titel) mit Leerzeichen voneinander getrennt sind.

### Hinzufügen eines neuen Titels
Geben Sie den Titel in die das Textfeld unten ein und klicken Sie auf "Hinzufügen". Auf Ihrem Desktop wird eine `JSON`-Datei angelegt, welche alle Titel mitsamt dem neuen Titel enthält.

## User Stories
### Story 1 
* Als Headhunter möchte ich gezielt Job-KandidatInnen förmlich mit Anrede kontaktieren, um auf persönlicher Ebene Kontakt aufzubauen. Hierfür ist eine generierte Briefanrede vonnöten.
* Prio: *hoch*
#### Akzeptanzkriterium: 
* Der Headhunter kann in der Anwendung den Namen einer Person eingeben und die Eingabe per Knopfdruck bestätigen. 
* Aus der Eingabe wird ein Vorschlag für eine standardisierte Briefanrede gegeben, die nachträglich angepasst werden kann.
### Story 2
* Als Mitarbeiter der Personalabteilung möchte ich, dass für neue Mitarbeiter der akademische Titel mit erfasst wird, damit dieser nicht mit nachträglichem Aufwand erfragt werden muss.
* Prio: *niedrig*
#### Akzeptanzkriterium: 
* Aus der Anrede muss der akademische Titel erkannt und in einem eigenen Feld angezeigt werden.
### Story 3
* Als Systemadministrator des CRM möchte ich, dass Mitarbeiter selbstständig neue Titel in die Software einpflegen können, da ansonsten Mehraufwand für die Administration entsteht.
* Prio: *mittel*
#### Akzeptanzkriterium: 
* Der User hat die Möglichkeit Titel, die die Anwendung nativ nicht kennt, zur Erkennung hinzuzufügen. 
* Neue Titel werden wie die nativen Titel erkannt und im Titelfeld angezeigt. 
### Story 4
* Als Mitarbeiter im Vertrieb und CRM nutzer möchte ich eine korrekte Ermittlung des Geschlechts basierend auf der Anrede-Eingabe, um den Kunden nicht zu verärgern. Hierbei ist mir eine korrekte Handhabung des Genderings wichtig.
* Prio *mittel*
#### Akzeptanzkriterium: 
* Das Geschlecht kann aus der Eingabe-Anrede ermittelt werden und in die formale Briefanrede überführt werden.
### Story 5
* Als Datenbank Administrator möchte ich eine Granularisierung der Anrede, um die Daten strukturiert persistieren zu können.  
* Prio *hoch*
#### Akzeptanzkriterium: 
* Einzelne Bestandteile der Anrede werden erkannt, korrekt getrennt und liegen in separaten Textfeldern vor. 

# Definition of Done
## Release
* Alle Akzeptanzkriterien für jede User-Story wurden Implementiert.
* Die Implementierten Funktionen wurden getestet, Tests mit den Beispielhaften Testdaten wurden durchgeführt. 
* Bugs und fehlende Features wurden überarbeitet und in der Gruppe ausgetauscht.
* Code wurde aussagekräftig kommentiert und die Software-Architektur festgehalten.
* Benutzerfreundlichkeit an Releaseversion getestet und verifiziert.  
* Release Note basierend auf Releaseversion verfasst. 
