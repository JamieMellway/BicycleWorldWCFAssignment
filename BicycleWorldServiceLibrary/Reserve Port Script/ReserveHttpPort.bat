rem Run as Administrator

rem netsh http show sslcert
rem netsh http show urlacl

rem netsh http delete urlacl url=https://+:4431/BicycleWorld/BicycleWorld.svc

rem netsh http add urlacl url=https://+:4431/BicycleWorld user=highpath\jmellway
rem netsh http add urlacl url=http://+:80/ user=jmellway
rem netsh http add urlacl url=http://+:8010/ user=jmellway
rem netsh http add urlacl url=https://+:8021/ user=jmellway
rem netsh http add urlacl url=https://+:8020/ user=jmellway


netsh http add urlacl url=http://+:9000/Services/MsdnRoles/ user=jmellway
 