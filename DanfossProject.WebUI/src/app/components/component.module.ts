import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { MatNativeDateModule } from '@angular/material';
import { SharedModule } from '../Shared/shared.module';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        SharedModule,
        MatNativeDateModule,
        BrowserModule,
        FormsModule
    ],
    declarations: [
        FooterComponent,
        NavbarComponent,
        SidebarComponent
      ],
      exports: [
        FooterComponent,
        NavbarComponent,
        SidebarComponent
      ]
})

export class ComponentsModule {

}
