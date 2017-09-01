"use strict";

function main() {
    
    var webdriver = require('selenium-webdriver');
    
    var desiredCaps = {
        browserName: '',
        platformName: 'WINDOWS',
        app: 'Microsoft.WindowsCalculator_8wekyb3d8bbwe!App'
    };
        
    //Open the app.
    var driver = new webdriver.Builder()
        .usingServer("http://localhost:4723")
        .withCapabilities(desiredCaps)
        .build();

    // Initialize the eyes SDK and set your private API key.
    var Eyes = require("eyes.selenium").Eyes;
    var eyes = new Eyes();
    
    eyes.setApiKey("your_applitools_key");

    try {
        
        //driver.findElement(webdriver.By.name('Clear')).click();
        //driver.findElement(webdriver.By.name('Seven')).click();
        
        // Start the test.
        eyes.open(driver, "Windows Calculator", "WinAppDriver JavaScript");

        // Visual testing.
        eyes.checkWindow("Equals 7");

        // End the test.
        eyes.close();

    } finally {

        // Close the app.
        driver.quit();

        // If the test was aborted before eyes.close was called, ends the test as aborted.
        eyes.abortIfNotClosed();

    }

}

main();