import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatDialogRef, MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { CreateWaterMeterComponent } from './create-water-meter/create-water-meter.component';
import { WaterMeter } from '../Shared/models/waterMeter.model';
import { EditWaterMeterComponent } from './edit-water-meter/edit-water-meter.component';
import { FilterService } from '../Shared/services/filter.service';
import { WaterMeterRepository } from '../Shared/repository/waterMeter.repository';
import { Subscription } from 'rxjs';

export interface WaterMeterTable {
  id: number;
  position: number;
  serial: string;
  counterValue: number;
}

@Component({
  selector: 'app-water-meter',
  templateUrl: './water-meter.component.html',
  styleUrls: ['./water-meter.component.css']
})
export class WaterMeterComponent implements OnInit, OnDestroy {

  waterMeters: WaterMeter[];
  load = false;

  displayedColumns: string[] = ['position', 'serial', 'counterValue', 'actions'];
  dataSource;
  createWaterMeterDialog: MatDialogRef<CreateWaterMeterComponent>;
  editWaterMeterDialog: MatDialogRef<EditWaterMeterComponent>;
  subscriptions: Subscription = new Subscription();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    public dialog: MatDialog,
    private waterMeterRepository: WaterMeterRepository,
    private filterService: FilterService
  ) {
  }

  ngOnInit() {
    this.subscriptions.add(this.waterMeterRepository.getWaterMeters()
      .subscribe(data => {
        this.waterMeters = data;
        this.dataSource = new MatTableDataSource<WaterMeterTable>(this.getBuildingTable());
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

  addWaterMeter() {
    this.createWaterMeterDialog = this.dialog.open(CreateWaterMeterComponent);
  }

  editWaterMeter(id) {
    this.editWaterMeterDialog = this.dialog.open(EditWaterMeterComponent, {
      data: {id: id}
    });
  }

  deleteWaterMeter(id) {
    this.waterMeters.splice(this.waterMeters.findIndex(e => e.Id === id), 1);
    this.dataSource.data = this.getBuildingTable();
    this.subscriptions.add(this.waterMeterRepository.deleteWaterMeter(id).subscribe());
  }

  getBuildingTable(): WaterMeterTable[] {
    let count = 1;
    // tslint:disable-next-line:prefer-const
    let data: WaterMeterTable[] = [];
    let temp;

    this.waterMeters.forEach(e => {
      temp = {id: e.Id, position: count++, serial: `${e.SerialNumber }`, counterValue: e.CounterValue };
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
    this.subscriptions.add(this.waterMeterRepository.getWaterMeters()
        .subscribe(data => {
          this.waterMeters = data;
          this.dataSource.data = this.getBuildingTable();
        }));
  }
}
