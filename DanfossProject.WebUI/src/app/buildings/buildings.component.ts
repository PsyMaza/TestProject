import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatDialog, MatDialogRef, MatPaginator, MatTableDataSource } from '@angular/material';
import { CreateBuildingComponent } from './create/create-building.component';
import { BuildingRepository } from '../Shared/repository/building.repository';
import { Subscription } from 'rxjs';
import { Building } from '../Shared/models/building.model';
import { FilterService } from '../Shared/services/filter.service';
import { EditBuildingComponent } from './edit-building/edit-building.component';

export interface DialogData {
  id: number;
}

export interface BuildigsTable {
  id: number;
  position: number;
  company: string;
  address: string;
  serial: string;
  counterValue: number;
}

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.css']
})
export class BuildingsComponent implements OnInit, OnDestroy {

  buildings: Building[] = [];
  load = false;

  displayedColumns: string[] = ['position', 'company', 'address', 'serial', 'counterValue', 'actions'];
  dataSource;
  createBuildingDialog: MatDialogRef<CreateBuildingComponent>;
  editBuildingDialog: MatDialogRef<EditBuildingComponent>;
  private subscriptions: Subscription = new Subscription();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    public dialog: MatDialog,
    private buildingRepository: BuildingRepository,
    private filterService: FilterService
  ) {
  }

  ngOnInit() {
    this.subscriptions.add(
      this.buildingRepository.getBuildings()
      .subscribe(data => {
        this.buildings = data;
        this.dataSource = new MatTableDataSource<BuildigsTable>(this.getBuildingTable());
        this.dataSource.paginator = this.paginator;
        this.load = true;
      }));

      this.subscriptions.add(
        this.filterService.filterAttribute
          .subscribe(filter => this.applyFilter(filter))
      );

      this.subscriptions.add(
        this.dialog._afterAllClosed
          .subscribe(() => this.refreshTable())
      );
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  addBuilding() {
    this.createBuildingDialog = this.dialog.open(CreateBuildingComponent);
  }

  editBuilding(id) {
    this.editBuildingDialog = this.dialog.open(EditBuildingComponent, {
      data: {id: id}
    });
  }

  deleteBuilding(id) {
    const itemSDIndex = this.dataSource.data.findIndex(obj => obj.Id === id);
    this.dataSource.data.splice(itemSDIndex, 1);
    this.dataSource.paginator = this.paginator;
    this.buildings.splice(this.buildings.findIndex(e => e.Id === id), 1);
    this.subscriptions.add(this.buildingRepository.deleteBuilding(id).subscribe());
  }

  getBuildingTable(): BuildigsTable[] {
    let count = 1;
    // tslint:disable-next-line:prefer-const
    let data: BuildigsTable[] = [];
    let temp;

    this.buildings.forEach(e => {
      temp = {id: e.Id, position: count++, company: e.Company, address: `${e.Address.ZIPCode},
        ${e.Address.Country}, ${e.Address.City}, ${e.Address.Street}, ${e.Address.HouseNumber}`,
        serial: `${e.WaterMeter ? e.WaterMeter.SerialNumber : ''}`, counterValue: e.WaterMeter ? e.WaterMeter.CounterValue : 0 };
        data.push(temp);
    });

    return data;
  }

  applyFilter(filterValue: string) {
    if (this.dataSource != null) {
      this.dataSource.filter = filterValue.trim().toLowerCase();
    }
  }

  refreshTable() {
    this.buildingRepository.getBuildings()
      .subscribe(data => {
        this.buildings = data;
        this.dataSource.data = this.getBuildingTable();
      });
  }
}
