import { browser, by, element, Key } from 'protractor';

export class AppPage {
  async Get(): Promise<void> {
    await browser.get(browser.baseUrl);
    await browser.sleep(2000);
  }
  async SetDate(DateIn: string): Promise<void> {
    await element(by.name('ride_date_input')).sendKeys(DateIn);
  }
  async SetTime(Time: string): Promise<void> {
    await element(by.name('ride_time_input')).sendKeys(Time);
  }
  async SetMiles(Miles: string): Promise<void> {
    await element(by.name('miles_input')).sendKeys(Miles);
  }
  async SetMinutes(Minutes: string): Promise<void> {
    await element(by.name('minutes_input')).sendKeys(Minutes);
  }
  getSubmitButton() {
    return element(by.name('submit_button'));
  }
  async GetTotalCostCellContents(): Promise<string> {
    return element(by.name('total_cost_cell')).getText() as Promise<string>;
  }
}
