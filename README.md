# WinAppDriver-Examples
Applitools Code Examples for WinAppDriver

Windows Application Driver is a service to support Selenium-like UI Test Automation on Windows Applications. This service supports testing **Universal Windows Platform (UWP)** and **Classic Windows (Win32)** apps on **Windows 10 PCs**. Windows Application Driver complies to the [JSON Wire Protocol](https://github.com/SeleniumHQ/selenium/wiki/JsonWireProtocol) standard and some application management functionalities defined by **Appium**. If you've been looking for better support for using [Appium](http://appium.io) to test Windows Applications, then this service is for you!

## Getting Started

### System Requirements

- Windows 10 PC
- Any Appium test runner (Samples and Tests in this repository use Microsoft Visual Studio as the test runner)

### Installing and Running Windows Application Driver

1. Download Windows Application Driver installer from <https://github.com/Microsoft/WinAppDriver/releases>
2. Run the installer on a Windows 10 machine where your application under test is installed and will be tested
3. Run `WinAppDriver.exe` from the installation directory (E.g. `C:\Program Files (x86)\Windows Application Driver`)

Windows Application Driver will then be running on the test machine listening to requests on the default IP address and port (`127.0.0.1:4723`). You can then run any of our [Tests](/Tests/) or [Samples](/Samples). `WinAppDriver.exe` can be configured to listen to a different IP address and port as follows:

```
WinAppDriver.exe 4727
WinAppDriver.exe 10.0.0.10 4725
WinAppDriver.exe 10.0.0.10 4723/wd/hub
```

> **Note**: You must run `WinAppDriver.exe` as **administrator** to listen to a different IP address and port.

### Running on a Remote Machine

Windows Application Driver can run remotely on any Windows 10 machine with `WinAppDriver.exe` installed and running. This *test machine* can then serve any JSON wire protocol commands coming from the *test runner* remotely through the network. Below are the steps to the one time setup for the *test machine* to receive inbound requests:

1. On the *test machine* you want to run the test application on, open up **Windows Firewall with Advanced Security**
   - Select **Inbound Rules** -> **New Rule...**
   - **Rule Type** -> **Port**
   - Select **TCP**
   - Choose specific local port (4723 is WinAppDriver standard)
   - **Action** -> **Allow the connection**
   - **Profile** -> select all
   - **Name** -> optional, choose name for rule (e.g. WinAppDriver remote)
2. Run `ipconfig.exe` to determine your machine's local IP address
   > **Note**: Setting `*` as the IP address command line option will cause it to bind to all bound IP addresses on the machine
3. Run `WinAppDriver.exe` as **administrator** with command line arguments as seen above specifying local IP and port
4. On the *test runner* machine where the runner and scripts are, update the the test script to point to the IP of the remote *test machine*
5. Execute the test script on the *test runner* to perform the test actions against the test application on the remote *test machine*.

### Running the examples
* C#
   1. Open `CalculatorTest.sln` in Visual Studio
   2. Add your Applitools API key in ScenarioStandard.cs
   3. Select **Test** > **Windows** > **Test Explorer**
   4. Select **Run All** on the test pane or through menu **Test** > **Run** > **All Tests**
    Once the project is successfully built, you can use the **TestExplorer** to pick and choose the test scenario(s) to run

* Java
   1. Open WinAppDriver project in Eclipe or intelliJ
   2. Build the project to download the maven dependencies.
   3. Open WinAppDriver > src > test > java > CalculatorTest.java
   4. Add your Applitools API key.
   5. Right click and select run test.

* JavaScript (Current does not work)
   1. Open calculator.js in your favorite editor.
   2. Add your applitools API key.
   3. In terminal run: `node calculator.js`

* Python
   1. Open calculator.py in your favorite editor.
   2. Add your applitools key.
   3. In terminal run: `python calculator.py`

* Ruby
   1. Open calculator.rb in your favorite editor.
   2. Add your applitools API Key.
   3. In terminal run: `rspec calculator.rb`
   
### Attaching to an Existing App Window

In some cases, you may want to test applications that are not launched in a conventional way like shown above. For instance, the Cortana application is always running and will not launch a UI window until triggered through **Start Menu** or a keyboard shortcut. In this case, you can create a new session in Windows Application Driver by providing the application top level window handle as a hex string (E.g. `0xB822E2`). This window handle can be retrieved from various methods including the **Desktop Session** mentioned above. This mechanism can also be used for applications that have unusually long startup times. Below is an example of creating a test session for the **Cortana** app after launching the UI using a keyboard shortcut and locating the window using the **Desktop Session**.

```c#
DesktopSession.Keyboard.SendKeys(Keys.Meta + "s" + Keys.Meta);

var CortanaWindow = DesktopSession.FindElementByName("Cortana");
var CortanaTopLevelWindowHandle = CortanaWindow.GetAttribute("NativeWindowHandle");
CortanaTopLevelWindowHandle = (int.Parse(CortanaTopLevelWindowHandle)).ToString("x"); // Convert to Hex

// Create session by attaching to Cortana top level window
DesiredCapabilities appCapabilities = new DesiredCapabilities();
appCapabilities.SetCapability("appTopLevelWindow", CortanaTopLevelWindowHandle);
CortanaSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

// Use the session to control Cortana
CortanaSession.FindElementByAccessibilityId("SearchTextBox").SendKeys("add");
```


## Supported Capabilities

Below are the capabilities that can be used to create Windows Application Driver session.

| Capabilities       	| Descriptions                                          	| Example                                               	|
|--------------------	|-------------------------------------------------------	|-------------------------------------------------------	|
| app                	| Application identifier or executable full path        	| Microsoft.MicrosoftEdge_8wekyb3d8bbwe!MicrosoftEdge   	|
| appArguments       	| Application launch arguments                          	| https://github.com/Microsoft/WinAppDriver             	|
| appTopLevelWindow  	| Existing application top level window to attach to    	| `0xB822E2`                                            	|
| appWorkingDir      	| Application working directory (Classic apps only)     	| `C:\Temp`                                             	|
| platformName       	| Target platform name                                  	| Windows                                               	|
| platformVersion    	| Target platform version                               	| 1.0                                                   	|


## Supported APIs

| HTTP   	| Path                                              	|
|--------	|---------------------------------------------------	|
| GET    	| /status                                           	|
| POST   	| /session                                          	|
| GET    	| /sessions                                         	|
| DELETE 	| /session/:sessionId                               	|
| POST   	| /session/:sessionId/appium/app/launch             	|
| POST   	| /session/:sessionId/appium/app/close              	|
| POST   	| /session/:sessionId/back                          	|
| POST   	| /session/:sessionId/buttondown                    	|
| POST   	| /session/:sessionId/buttonup                      	|
| POST   	| /session/:sessionId/click                         	|
| POST   	| /session/:sessionId/doubleclick                   	|
| POST   	| /session/:sessionId/element                       	|
| POST   	| /session/:sessionId/elements                      	|
| POST   	| /session/:sessionId/element/active                	|
| GET    	| /session/:sessionId/element/:id/attribute/:name   	|
| POST   	| /session/:sessionId/element/:id/clear             	|
| POST   	| /session/:sessionId/element/:id/click             	|
| GET    	| /session/:sessionId/element/:id/displayed         	|
| GET    	| /session/:sessionId/element/:id/element           	|
| GET    	| /session/:sessionId/element/:id/elements          	|
| GET    	| /session/:sessionId/element/:id/enabled           	|
| GET    	| /session/:sessionId/element/:id/equals            	|
| GET    	| /session/:sessionId/element/:id/location          	|
| GET    	| /session/:sessionId/element/:id/location_in_view  	|
| GET    	| /session/:sessionId/element/:id/name              	|
| GET    	| /session/:sessionId/element/:id/screenshot        	|
| GET    	| /session/:sessionId/element/:id/selected          	|
| GET    	| /session/:sessionId/element/:id/size              	|
| GET    	| /session/:sessionId/element/:id/text              	|
| POST   	| /session/:sessionId/element/:id/value             	|
| POST   	| /session/:sessionId/forward                       	|
| POST   	| /session/:sessionId/keys                          	|
| GET    	| /session/:sessionId/location                      	|
| POST   	| /session/:sessionId/moveto                        	|
| GET    	| /session/:sessionId/orientation                   	|
| GET    	| /session/:sessionId/screenshot                    	|
| GET    	| /session/:sessionId/source                        	|
| POST   	| /session/:sessionId/timeouts                      	|
| POST   	| /session/:sessionId/timeouts/implicit_wait        	|
| GET    	| /session/:sessionId/title                         	|
| POST   	| /session/:sessionId/touch/click                   	|
| POST   	| /session/:sessionId/touch/doubleclick             	|
| POST   	| /session/:sessionId/touch/down                    	|
| POST   	| /session/:sessionId/touch/flick                   	|
| POST   	| /session/:sessionId/touch/longclick               	|
| POST   	| /session/:sessionId/touch/move                    	|
| POST   	| /session/:sessionId/touch/multi/perform           	|
| POST   	| /session/:sessionId/touch/scroll                  	|
| POST   	| /session/:sessionId/touch/up                      	|
| GET    	| /session/:sessionId/window                        	|
| DELETE 	| /session/:sessionId/window                        	|
| POST   	| /session/:sessionId/window                        	|
| GET    	| /session/:sessionId/window/handles                	|
| POST   	| /session/:sessionId/window/maximize               	|
| POST   	| /session/:sessionId/window/size                   	|
| GET    	| /session/:sessionId/window/size                   	|
| POST   	| /session/:sessionId/window/:windowHandle/size     	|
| GET    	| /session/:sessionId/window/:windowHandle/size     	|
| POST   	| /session/:sessionId/window/:windowHandle/position 	|
| GET    	| /session/:sessionId/window/:windowHandle/position 	|
| POST   	| /session/:sessionId/window/:windowHandle/maximize 	|
| GET    	| /session/:sessionId/window_handle                 	|
| GET    	| /session/:sessionId/window_handles                	|


## Supported Locators to Find UI Elements

Windows Application Driver supports various locators to find UI element in the application session. The table below shows all supported locator strategies with their corresponding UI element attributes shown in **inspect.exe**.

| Client API                   	| Locator Strategy 	| Matched Attribute in inspect.exe       	| Example      	|
|------------------------------	|------------------	|----------------------------------------	|--------------	|
| FindElementByAccessibilityId 	| accessibility id 	| AutomationId                           	| AppNameTitle 	|
| FindElementByClassName       	| class name       	| ClassName                              	| TextBlock    	|
| FindElementById              	| id               	| RuntimeId (decimal)                    	| 42.333896.3.1	|
| FindElementByName            	| name             	| Name                                   	| Calculator   	|
| FindElementByTagName         	| tag name         	| LocalizedControlType (upper camel case)	| Text         	|


## Inspecting UI Elements

The latest Microsoft Visual Studio version by default includes the Windows SDK with a great tool to inspect the application you are testing. This tool allows you to see every UI element/node that you can query using Windows Application Driver. This **inspect.exe** tool can be found under the Windows SDK folder which is typically `C:\Program Files (x86)\Windows Kits\10\bin\x86`

More detailed documentation on Inspect is available on MSDN <"https://msdn.microsoft.com/library/windows/desktop/dd318521(v=vs.85).aspx">.