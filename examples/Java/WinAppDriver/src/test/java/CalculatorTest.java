import com.applitools.eyes.*;
import com.applitools.eyes.selenium.Eyes;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.openqa.selenium.remote.RemoteWebDriver;
import java.net.URL;
import static org.junit.Assert.assertEquals;

public class CalculatorTest {

    public static String applitoolsKey = "your_applitools_key";

    private Eyes eyes = new Eyes();
    private WebDriver driver;

    @Before
    public void setUp() throws Exception {
        eyes.setApiKey(applitoolsKey);
        DesiredCapabilities capability = new DesiredCapabilities();
        capability.setCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
        String winAppDriverUrl = "http://localhost:4723";
        driver = new RemoteWebDriver(new URL(winAppDriverUrl), capability);
        driver.manage().window().setSize(new Dimension(700,700)); //For responsive design testing...
    }

    @Test
    public void CalculatorAdd() throws Exception {
        eyes.open(driver, "Windows Calculator App", "WinAppDriver Java");
        driver.findElement(By.name("Clear")).click();
        driver.findElement(By.name("One")).click();
        driver.findElement(By.name("Plus")).click();
        driver.findElement(By.name("Seven")).click();
        driver.findElement(By.name("Equals")).click();
        eyes.checkWindow("Equals 8");
        TestResults results = eyes.close();
        assertEquals(true, results.isPassed());
    }

    @After
    public void tearDown() throws Exception {
        driver.quit();
        eyes.abortIfNotClosed();
    }
}