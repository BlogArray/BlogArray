import { Directive, ElementRef, HostBinding, Input, Renderer2, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[tw-input]'
})
export class TwInputDirective {
  /**
   * @Input() size: 'sm' | 'md' | 'lg'
   * Defines the size of the input. The default size is 'md' (medium). 
   * Available options:
   * - 'sm' : Small input
   * - 'md' : Medium input (default)
   * - 'lg' : Large input
   */
  @Input() size: 'sm' | 'md' | 'lg' = 'md';

  /**
   * @Input() disabled: boolean
   * Controls whether the input is disabled. When true, the input will not be interactive.
   */
  @Input() disabled: boolean = false;

  /**
   * @Input() readonly: boolean
   * Controls whether the input is read-only. When true, the input will allow focus but not editing.
   */
  @Input() readonly: boolean = false;

  /**
   * @Input() valid: boolean
   * Indicates whether the input is valid. If true, applies valid styles.
   */
  @Input() valid: boolean = false;

  /**
   * @Input() error: boolean
   * Indicates whether there is an error with the input. If true, applies error styles.
   */
  @Input() error: boolean = false;

  @HostBinding('class') inputClass = '';

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  ngOnInit() {
    this.setInputClasses();
  }

  /**
   * ngOnChanges
   * Lifecycle hook called when input properties change.
   * It updates the Tailwind CSS classes dynamically whenever there is a change in size, disabled, or readonly state.
   * 
   * @param changes: SimpleChanges - Contains information about the changed inputs
   */
  ngOnChanges(_changes: SimpleChanges) {
    this.setInputClasses();
  }

  private setInputClasses() {
    // Base Tailwind classes for the input
    let baseClasses = 'block w-full text-sm rounded-lg p-2.5 focus:outline-none';

    // Size-based classes
    const sizeClasses = {
      sm: 'px-2 py-1 text-sm',
      md: 'px-3 py-2 text-base',
      lg: 'px-4 py-3 text-lg'
    };

    // State-based classes
    const stateClasses = this.disabled
      ? 'bg-gray-200 text-gray-500 cursor-not-allowed'
      : this.readonly
        ? 'bg-gray-100 text-gray-700 cursor-not-allowed'
        : 'bg-white text-gray-900';

    // Default focus and border styles
    let borderClasses = 'border border-gray-300 focus:ring focus:ring-blue-300 focus:border-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-300 dark:focus:border-blue-300';

    // Apply valid or error styles if applicable
    if (this.valid) {
      borderClasses = 'bg-green-50 border border-green-500 text-green-900 dark:text-green-400 placeholder-green-700 dark:placeholder-green-500 focus:ring focus:ring-green-300 focus:border-green-300 dark:bg-gray-700 dark:border-green-300';
    } else if (this.error) {
      borderClasses = 'bg-red-50 border border-red-500 text-red-900 placeholder-red-700 focus:ring focus:ring-red-300 focus:border-red-300 dark:bg-gray-700 dark:text-red-500 dark:placeholder-red-500 dark:border-red-300';
    }

    // Combine all the classes
    this.inputClass = `${baseClasses} ${sizeClasses[this.size]} ${stateClasses} ${borderClasses}`;

    // Set the disabled/readonly attributes as well
    this.setAttributes();
  }

  /**
   * setAttributes
   * A private method that sets or removes the 'disabled' and 'readonly' attributes directly on the input element.
   * It ensures that the input behaves as expected based on the values of the disabled and readonly inputs.
   */
  private setAttributes() {
    if (this.disabled) {
      this.renderer.setAttribute(this.el.nativeElement, 'disabled', 'true');
    } else {
      this.renderer.removeAttribute(this.el.nativeElement, 'disabled');
    }

    if (this.readonly) {
      this.renderer.setAttribute(this.el.nativeElement, 'readonly', 'true');
    } else {
      this.renderer.removeAttribute(this.el.nativeElement, 'readonly');
    }
  }
}
