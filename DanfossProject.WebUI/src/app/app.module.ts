import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { SharedModule } from './Shared/shared.module';
import { MatNativeDateModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from './components/component.module';
import { BuildingsComponent } from './buildings/buildings.component';
import { WaterMeterComponent } from './water-meter/water-meter.component';
import { CreateBuildingComponent } from './buildings/create/create-building.component';
import { AppRouterModule } from './router.module';
import { CreateWaterMeterComponent } from './water-meter/create-water-meter/create-water-meter.component';
import { HttpClientModule } from '@angular/common/http';
import { BuildingRepository } from './Shared/repository/building.repository';
import { WaterMeterRepository } from './Shared/repository/waterMeter.repository';
import { ConfigService } from './Shared/util/config.service';
import { FilterService } from './Shared/services/filter.service';
import { EditBuildingComponent } from './buildings/edit-building/edit-building.component';
import { EditWaterMeterComponent } from './water-meter/edit-water-meter/edit-water-meter.component';




@NgModule({
  declarations: [
    AppComponent,
    BuildingsComponent,
    WaterMeterComponent,
    CreateBuildingComponent,
    CreateWaterMeterComponent,
    EditBuildingComponent,
    EditWaterMeterComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ComponentsModule,
    SharedModule,
    MatNativeDateModule,
    AppRouterModule,
    ReactiveFormsModule
  ],
  providers: [
    ConfigService,
    BuildingRepository,
    WaterMeterRepository,
    FilterService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
