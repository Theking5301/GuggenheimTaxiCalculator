import { NumberValidatorDirective } from './number-validator.directive';

describe('NumberValidatorDirective', () => {
  it('should create an instance', () => {
    const directive = new NumberValidatorDirective(undefined, undefined);
    expect(directive).toBeTruthy();
  });
});
