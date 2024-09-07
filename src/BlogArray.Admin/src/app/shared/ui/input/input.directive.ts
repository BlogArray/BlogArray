import { Directive, ElementRef, HostBinding, Input, Renderer2, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[tw-input]'
})
/**
 * Directive selector to be used as an attribute on input elements
 */
export class InputDirective {
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
   * @HostBinding('class') inputClass: string
   * Dynamically binds a string of classes to the input element based on the size, disabled, and readonly properties.
   * This allows Tailwind CSS classes to be applied to the input element.
   */
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

  /**
   * setInputClasses
   * A private method that constructs the necessary Tailwind CSS classes based on the size, disabled, and readonly inputs.
   * The resulting classes are assigned to the `inputClass` variable, which is then applied to the host element.
   */
  private setInputClasses() {
    // Reset the class attribute before adding new ones
    let baseClasses = 'block w-full text-gray-900 border border-gray-300 rounded-lg bg-gray-50 text-base focus:ring-blue-300 focus:outline-none focus:ring focus:border-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-300 dark:focus:border-blue-300';

    // Apply size classes
    const sizeClasses = {
      sm: 'px-2 py-1 text-sm',
      md: 'px-3 py-2 text-base',
      lg: 'px-4 py-3 text-lg'
    };

    // Apply disabled and readonly styles
    const stateClasses = this.disabled
      ? 'bg-gray-200 text-gray-500 cursor-not-allowed'
      : this.readonly
        ? 'bg-gray-100 text-gray-700 cursor-not-allowed'
        : 'bg-white text-gray-900';

    // Combine all classes
    this.inputClass = `${baseClasses} ${sizeClasses[this.size]} ${stateClasses}`;

    // Set the disabled/readonly attributes as well
    this.setAttributes();
  }

  /**
   * setAttributes
   * A private method that sets or removes the 'disabled' and 'readonly' attributes directly on the input element.
   * It ensures that the input behaves as expected based on the values of the disabled and readonly inputs.
   */
  private setAttributes() {
    // Set disabled attribute if applicable
    if (this.disabled) {
      this.renderer.setAttribute(this.el.nativeElement, 'disabled', 'true');
    } else {
      this.renderer.removeAttribute(this.el.nativeElement, 'disabled');
    }

    // Set readonly attribute if applicable
    if (this.readonly) {
      this.renderer.setAttribute(this.el.nativeElement, 'readonly', 'true');
    } else {
      this.renderer.removeAttribute(this.el.nativeElement, 'readonly');
    }
  }
}
