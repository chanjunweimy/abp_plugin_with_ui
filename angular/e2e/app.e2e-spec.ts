import { MainProjectTemplatePage } from './app.po';

describe('abp-project-name-template App', function() {
  let page: MainProjectTemplatePage;

  beforeEach(() => {
    page = new MainProjectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
