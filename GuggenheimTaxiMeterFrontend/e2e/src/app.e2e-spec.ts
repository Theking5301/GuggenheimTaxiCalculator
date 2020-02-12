import { AppPage } from './app.po';
import { browser, logging } from 'protractor';


describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(async () => {
    page = new AppPage();
  });

  it('validate provided example data', async () => {
    await page.Get();
    await page.SetDate('10082010');
    await page.SetTime('1730PM');
    await page.SetMiles('2');
    await page.SetMinutes('5');
    await page.getSubmitButton().click();
    await browser.sleep(5000);
    expect(await page.GetTotalCostCellContents()).toEqual('$9.75');
  });

  afterEach(async () => {
    // Assert that there are no errors emitted from the browser
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining({
      level: logging.Level.SEVERE,
    } as logging.Entry));
  });
});
