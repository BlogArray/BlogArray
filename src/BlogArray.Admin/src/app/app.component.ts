import { Component } from '@angular/core';
import '@cds/core/icon/register.js';
import {
  ClarityIcons, userIcon, dashboardIcon, usersIcon, pencilIcon,
  fileIcon, cogIcon, colorPaletteIcon, blocksGroupIcon,
  tagsIcon, fileGroupIcon
} from '@cds/core/icon';

ClarityIcons.addIcons(userIcon, dashboardIcon, usersIcon, pencilIcon,
  fileIcon, cogIcon, colorPaletteIcon, blocksGroupIcon,
  tagsIcon, fileGroupIcon);
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'BlogArray.Admin';
}
