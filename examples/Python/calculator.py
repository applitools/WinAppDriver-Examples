import unittest
from selenium import webdriver
#pip install eyes-selenium==3.5
from applitools.eyes import Eyes 

class SimpleCalculatorTests(unittest.TestCase):

	def setUp(self):
		desired_caps = {}
		desired_caps["app"] = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App"
		self.driver = webdriver.Remote(command_executor='http://127.0.0.1:4723', desired_capabilities= desired_caps)
		self.eyes = Eyes()
		self.eyes.api_key = "your_applitools_key"
		self.driver.set_window_size(700, 700) #set size for responsive design testing...

	def tearDown(self):
		self.driver.quit()
		self.eyes.abort_if_not_closed()

	def test_initialize(self):
		self.eyes.open(driver = self.driver, app_name = 'Windows Calculator', test_name = 'Python Windows Calculator Test')
		self.driver.find_element_by_name("Clear").click()
		self.driver.find_element_by_name("Seven").click()
		self.eyes.check_window("Displays 7")
		self.eyes.close()

	def test_addition(self):
		self.eyes.open(driver = self.driver, app_name = 'Windows Calculator', test_name = 'Python Windows Calculator Test')
		self.driver.find_element_by_name("Clear").click()
		self.driver.find_element_by_name("One").click()
		self.driver.find_element_by_name("Plus").click()
		self.driver.find_element_by_name("Seven").click()
		self.driver.find_element_by_name("Equals").click()
		self.eyes.check_window("Displays 8")
		self.eyes.close()

if __name__ == '__main__':
    suite = unittest.TestLoader().loadTestsFromTestCase(SimpleCalculatorTests)
    unittest.TextTestRunner(verbosity=2).run(suite)