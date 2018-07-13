import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { BuildingsComponent } from './buildings/buildings.component';
import { WaterMeterComponent } from './water-meter/water-meter.component';
import { CreateBuildingComponent } from './buildings/create/create-building.component';
import { CreateWaterMeterComponent } from './water-meter/create-water-meter/create-water-meter.component';
import { EditBuildingComponent } from './buildings/edit-building/edit-building.component';
import { EditWaterMeterComponent } from './water-meter/edit-water-meter/edit-water-meter.component';

const appRoutes: Routes = [
    { path: '', redirectTo: 'buildings', pathMatch: 'full' },
    { path: 'buildings', component: BuildingsComponent },
    { path: 'waterMeters', component: WaterMeterComponent },
    { path: 'add-building', component: CreateBuildingComponent },
    { path: 'add-WaterMeter', component: CreateWaterMeterComponent },
    { path: 'edit-building', component: EditBuildingComponent },
    { path: 'edit-waterMeter', component: EditWaterMeterComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
})

export class AppRouterModule {
}
