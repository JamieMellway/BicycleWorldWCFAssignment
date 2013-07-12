cd GeneratedXml
svcutil /t:metadata ..\..\BicycleWorldServiceLibrary\bin\Debug\BicycleWorldServiceLibrary.dll
svcutil /namespace:*,BicycleWorldServiceModelClient.BicycleWorldService *.wsdl *.xsd /ct:System.Collections.Generic.List`1 /out:BicycleWorldProxy.cs /enabledatabinding
copy *.cs ..
copy output.config ..
cd ..
