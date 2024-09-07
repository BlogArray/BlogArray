import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[tw-form-label]'
})
export class FormLabelDirective {

  @HostBinding('class') buttonClasses: string = '';

  constructor() { }

  ngOnInit() {
    this.setButtonClasses();
  }

  private setButtonClasses() {
    // Base Tailwind CSS classes
    let baseClasses = 'block mb-2 text-sm font-medium text-gray-900 dark:text-white';

    this.buttonClasses = `${baseClasses}`;
  }
}