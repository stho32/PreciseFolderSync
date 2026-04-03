---
id: R00003
titel: "Selektives Ignorieren"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Selektives Ignorieren

## Beschreibung

Ueber die CLI-Option `--ignore` koennen Dateien und Verzeichnisse von der Synchronisation ausgeschlossen werden. Die zu ignorierenden Eintraege werden als komma-separierte Liste uebergeben. Verzeichnisse werden durch einen abschliessenden Backslash (`\`) gekennzeichnet und fuehren dazu, dass alle Dateien und Unterverzeichnisse unterhalb dieses Pfades ignoriert werden. Der Vergleich erfolgt case-insensitive, sodass Gross-/Kleinschreibung keine Rolle spielt.

## Akzeptanzkriterien

- [x] CLI-Option `--ignore` / `-i` ist verfuegbar und optional
- [x] Mehrere Eintraege werden komma-separiert uebergeben
- [x] Verzeichnisse werden mit abschliessendem Backslash markiert (z.B. `subdir\`)
- [x] Bei Verzeichnis-Ignore werden alle Dateien und Unterverzeichnisse unterhalb des Pfades ignoriert (StartsWith-Pruefung)
- [x] Bei Datei-Ignore wird ein exakter Pfadvergleich durchgefuehrt (Equals-Pruefung)
- [x] Der Vergleich ist case-insensitive (StringComparison.OrdinalIgnoreCase)
- [x] Ignorierte Operationen werden in grauer Schrift mit "IGNORED" in der Konsole ausgegeben

## Technische Details

- `Source/Pfs/Pfs.BL/CommandLineArguments/CommandLineOptions.cs` - Definition der `Ignore`-Option mit `Separator = ','`
- `Source/Pfs/Pfs/Program.cs` - Methode `ShouldIgnoreOperation()` prueft ob ein relativer Pfad ignoriert werden soll; unterscheidet zwischen Verzeichnis-Ignore (EndsWith `\`, StartsWith-Vergleich) und Datei-Ignore (Equals-Vergleich)
- `Source/Pfs/Pfs.BL/CommandLineArguments/CommandLineArgumentsParser.cs` - Parsing der Kommandozeilenargumente inkl. Ignore-Liste
