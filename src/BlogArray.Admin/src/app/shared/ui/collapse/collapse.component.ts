import { Component, ElementRef, HostListener, Input, TemplateRef } from '@angular/core';

@Component({
  selector: 'tw-collapse',
  templateUrl: './collapse.component.html',
  styleUrl: './collapse.component.css'
})
export class CollapseComponent {
  @Input() triggerTemplate: TemplateRef<any> | null;
  @Input() contentTemplate: TemplateRef<any> | null;
  @Input() isOpen: boolean = false;

  constructor(private elementRef: ElementRef) { }

  // Toggles the dropdown
  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }

}
