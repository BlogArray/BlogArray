import { Directive, ElementRef, HostBinding, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[tw-form-label]'
})
export class TwFormLabelDirective {

  /**
   * @Input() required: boolean
   * Indicates whether the field is required or not. If true, appends an asterisk (*) to the label.
   */
  @Input() required: boolean = false;

  @HostBinding('class') labelClasses: string = '';

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  ngOnInit() {
    this.setLabelClasses();
    this.setRequiredIndicator();
  }

  /**
   * Sets the base Tailwind CSS classes to the label element.
   */
  private setLabelClasses() {
    let baseClasses = 'block mb-2 text-sm font-medium text-gray-900 dark:text-white';
    this.labelClasses = baseClasses;
  }

  /**
   * Adds an asterisk (*) to the label text if the required property is true.
   */
  private setRequiredIndicator() {
    if (this.required) {
      const labelText = this.el.nativeElement.innerHTML;
      // Append an asterisk (*) with a red color to the label if it's required
      this.renderer.setProperty(this.el.nativeElement, 'innerHTML', `${labelText} <span class="text-red-500">*</span>`);
    }
  }
}
