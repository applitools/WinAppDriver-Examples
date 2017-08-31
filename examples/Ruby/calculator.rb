#https://www.maketecheasier.com/access-windowsapps-folder-windows-10/
#https://github.com/Microsoft/WinAppDriver

gem 'eyes_selenium', '=3.10.1'
require 'eyes_selenium'
require 'rspec'

describe 'Calculator App' do

  before :all do
    caps = { app: "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App" }
    browser = Selenium::WebDriver.for(:remote, :url => "http://127.0.0.1:4723/", :desired_capabilities => caps )
    @eyes = Applitools::Selenium::Eyes.new
    @eyes.api_key = "your_applitools_key"
    @driver = @eyes.open(driver: browser, app_name: "Calculator App", test_name: "Ruby Calculator Test")
    @driver.manage.window.resize_to(700, 700) #For responsive design testing...
  end
  
  after :each do
    @eyes.abort_if_not_closed
    @driver.quit
  end
  
  it 'Addition' do
    @driver.find_element(:name, "Clear").click
    @driver.find_element(:name, "One").click
    @driver.find_element(:name, "Plus").click
    @driver.find_element(:name, "Seven").click
    @driver.find_element(:name, "Equals").click
    @eyes.check_window "equals 8"
     
    results = @eyes.close(false)
    expect(results.passed?).to eq true
  end
end