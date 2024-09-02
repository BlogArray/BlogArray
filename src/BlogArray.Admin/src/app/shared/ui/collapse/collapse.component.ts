import { Component, Input } from '@angular/core';

@Component({
  selector: 'tw-collapse',
  templateUrl: './collapse.component.html',
  styleUrl: './collapse.component.css'
})
export class CollapseComponent {
  @Input() isOpen: boolean = false;

  constructor() { }

  // Toggles the dropdown
  toggleCollapse() {
    this.isOpen = !this.isOpen;
  }

  // Explicitly open the dropdown
  open() {
    this.isOpen = true;
  }

  // Explicitly close the dropdown
  close() {
    this.isOpen = false;
  }

}
