import { PnPPage } from './app.po';

describe('pn-p App', () => {
  let page: PnPPage;

  beforeEach(() => {
    page = new PnPPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
