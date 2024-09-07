import { Directive, ElementRef, HostBinding, Input, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[tw-button]'
})
export class ButtonDirective {
  /**
   * The variant of the button. Possible values are:
   * 'primary', 'secondary', 'success', 'danger', 'warning', 'info', 'light', and 'dark'.
   * Default is 'primary'.
   */
  @Input() variant: 'primary' | 'secondary' | 'success' | 'danger' | 'warning' | 'info' | 'light' | 'dark' = 'primary';

  /**
   * The size of the button. Possible values are:
   * 'xs' for extra small, 'sm' for small, 'md' for medium, 'lg' for large, and 'xl' for extra large.
   * Default is 'md'.
   */
  @Input() size: 'xs' | 'sm' | 'md' | 'lg' | 'xl' = 'md';

  /**
  * If true, the button will be displayed as a block element (full-width).
  * Default is false.
  */
  @Input() block: boolean = false;

  /**
  * Whether the button is disabled. When true, the button is not clickable.
  * Default is false.
  */
  @Input() disabled: boolean = false;

  /**
   * If true, the button will have an outlined style.
   * Default is false.
   */
  @Input() outlined: boolean = false;

  /**
   * If true, the button will have fully rounded corners.
   * Default is false.
   */
  @Input() rounded: boolean = false;

  @HostBinding('class') buttonClasses: string = '';

  /**
   * Applies the 'disabled' attribute to the button element when either the 'disabled' or 'loading' input is true.
   * This makes the button inaccessible when it is disabled or loading.
   */
  @HostBinding('attr.disabled') get isDisabled() {
    return this.disabled ? true : null;
  }

  private initialContent: string = '';

  constructor(private el: ElementRef) { }

  ngOnInit() {
    // Save the initial content of the button
    this.initialContent = this.el.nativeElement.innerHTML.trim();
    this.setButtonClasses();
    console.log(this.initialContent)
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['loading'] || changes['disabled']) {
      this.setButtonClasses();
    }
  }

  private setButtonClasses() {
    // Base Tailwind CSS classes
    let baseClasses = 'inline-flex items-center border justify-center font-medium focus:outline-none focus:ring transition-colors duration-300 ';

    // Size classes
    let sizeClasses = this.size === 'xs' ? 'px-3 py-2 text-xs font-medium' : this.size === 'sm' ? 'px-3 py-2 text-sm font-medium ' : this.size === 'lg' ? 'px-5 py-3 text-base font-medium ' : this.size === 'xl' ? 'px-6 py-3.5 text-base font-medium ' : 'px-5 py-2.5 text-sm ';

    // Variant classes (filled or outlined)
    const variantClasses = this.outlined
      ? {
        primary: 'text-blue-700 hover:text-white border-blue-700 hover:bg-blue-800 focus:ring-blue-300 text-center me-2 mb-2 dark:border-blue-500 dark:text-blue-500 dark:hover:text-white dark:hover:bg-blue-500 dark:focus:ring-blue-800',
        secondary: 'border-gray-600 text-gray-600 hover:bg-gray-600 hover:text-white me-2 mb-2',
        success: 'text-green-700 hover:text-white border-green-700 hover:bg-green-800 focus:ring-green-300 text-center me-2 mb-2 dark:border-green-500 dark:text-green-500 dark:hover:text-white dark:hover:bg-green-600 dark:focus:ring-green-800',
        danger: 'text-red-700 hover:text-white border-red-700 hover:bg-red-800 focus:ring-red-300 text-center me-2 mb-2 dark:border-red-500 dark:text-red-500 dark:hover:text-white dark:hover:bg-red-600 dark:focus:ring-red-900',
        warning: 'text-yellow-400 hover:text-white border-yellow-400 hover:bg-yellow-500 focus:ring-yellow-300 text-center me-2 mb-2 dark:border-yellow-300 dark:text-yellow-300 dark:hover:text-white dark:hover:bg-yellow-400 dark:focus:ring-yellow-900',
        info: 'text-purple-700 hover:text-white border-purple-700 hover:bg-purple-800 focus:ring-purple-300 text-center me-2 mb-2 dark:border-purple-400 dark:text-purple-400 dark:hover:text-white dark:hover:bg-purple-500 dark:focus:ring-purple-900',
        light: 'border-gray-200 text-gray-600 hover:bg-gray-200 me-2 mb-2',
        dark: 'text-gray-900 hover:text-white border-gray-800 hover:bg-gray-900 focus:ring-gray-300 text-center me-2 mb-2 dark:border-gray-600 dark:text-gray-400 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-800'
      }
      : {
        primary: 'text-white bg-blue-700 border-blue-600 hover:bg-blue-800 focus:ring-blue-300 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800',
        secondary: 'text-gray-900 bg-white border-gray-200 hover:bg-gray-100 me-2 mb-2 hover:text-blue-700 focus:z-10 focus:ring-gray-100 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700',
        success: 'text-white bg-green-700 hover:bg-green-800 focus:ring-green-300 me-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800',
        danger: 'text-white bg-red-700 hover:bg-red-800 focus:ring-red-300 me-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900',
        warning: 'text-white bg-yellow-400 hover:bg-yellow-500 focus:ring-yellow-300 me-2 mb-2 dark:focus:ring-yellow-900',
        info: 'text-white bg-purple-700 hover:bg-purple-800 focus:ring-purple-300 me-2 mb-2 dark:bg-purple-600 dark:hover:bg-purple-700 dark:focus:ring-purple-900',
        light: 'text-gray-900 bg-white border-gray-300 hover:bg-gray-100 focus:ring-gray-100 me-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700',
        dark: 'text-white bg-gray-800 hover:bg-gray-900 focus:ring-gray-300 me-2 mb-2 dark:bg-gray-800 dark:hover:bg-gray-700 dark:focus:ring-gray-700 dark:border-gray-700'
      };

    // Disabled classes
    const disabledClasses = this.disabled ? 'opacity-50 cursor-not-allowed ' : '';

    // Block button
    const blockClasses = this.block ? 'w-full ' : '';

    // Rounded corners
    const roundedClasses = this.rounded ? 'rounded-full ' : 'rounded-lg ';

    this.buttonClasses = `${baseClasses} ${sizeClasses} ${blockClasses} ${roundedClasses} ${variantClasses[this.variant]} ${disabledClasses}`;
  }
}
