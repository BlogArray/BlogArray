import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[tw-form]'
})
export class FormDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  ngOnInit() {
    this.applyLayoutClasses();
  }

  /**
   * Dynamically applies Tailwind CSS classes based on the layout input.
   */
  private applyLayoutClasses() {
    let formElement = this.el.nativeElement;

    // Reset existing layout classes
    this.renderer.removeClass(formElement, 'flex');
    this.renderer.removeClass(formElement, 'flex-col');
    this.renderer.removeClass(formElement, 'space-y-4');
    this.renderer.removeClass(formElement, 'space-x-4');
    this.renderer.removeClass(formElement, 'items-center');

    this.renderer.addClass(formElement, 'flex');
    this.renderer.addClass(formElement, 'flex-col');
    this.renderer.addClass(formElement, 'space-y-4'); // Stack fields vertically with space
  }
}
