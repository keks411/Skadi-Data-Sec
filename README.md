<h1># Skadi-Data-Sec</h1>

IOC-Scanner based on Loki-Scanner from Florian Roth (Neo23x0), Sysinternal, Windows-Tools and scripts

<h2>Version >=1.5</h2>
From now on it is possible to add custom IOCs to Loki/Skadi and set few options via config.ini. The following files in the skadi-folder can be modified:
<p>- config.ini
<p>- c2-iocs.txt
<p>- filename-iocs.txt
<p>- hash-iocs.txt

Those will then be added to loki.

Test OS:
- Windows 10 Pro
- Server 2016
- Server 2019

Working:
- loki
- autorunsc
- tcpvcon
- pslist
- handle64
- evtx dump

Roadmap:

- Create html-report
- Self-log

Optional (for DP-Framework:

x64: https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.400-windows-x64-installer

x86: https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.400-windows-x86-installer
