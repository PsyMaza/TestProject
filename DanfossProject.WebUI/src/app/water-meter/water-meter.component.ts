import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { CreateWaterMeterComponent } from './create-water-meter/create-water-meter.component';
import { WaterMeter } from '../Shared/models/waterMeter.model';
import { EditWaterMeterComponent } from './edit-water-meter/edit-water-meter.component';
import { FilterService } from '../Shared/services/filter.service';
import { WaterMeterRepository } from '../Shared/repository/waterMeter.repository';

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
export class WaterMeterComponent implements OnInit {

  waterMeters: WaterMeter[];
  load = false;

  displayedColumns: string[] = ['position', 'serial', 'counterValue', 'actions'];
  dataSource;
  createWaterMeterDialog: MatDialogRef<CreateWaterMeterComponent>;
  editWaterMeterDialog: MatDialogRef<EditWaterMeterComponent>;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    public dialog: MatDialog,
    private waterMeterRepository: WaterMeterRepository,
    private filterService: FilterService
  ) {
  }

  ngOnInit() {
    this.waterMeterRepository.getWaterMeters()
      .subscribe(data => {
        this.waterMeters = data;
        this.dataSource = new MatTableDataSource<WaterMeterTable>(this.getBuildingTable());
        this.dataSource.paginator = this.paginator;
        this.load = true;
      });

      this.filterService.filterAttribute
      .subscribe(filter => this.applyFilter(filter));
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
    this.dataSource.data.splice();
    const itemSDIndex = this.dataSource.data.findIndex(obj => obj.Id === id);
    this.dataSource.data.splice(itemSDIndex, 1);
    this.dataSource.paginator = this.paginator;
    this.waterMeters.splice(this.waterMeters.findIndex(e => e.Id === id), 1);
    this.waterMeterRepository.deleteWaterMeter(id).subscribe();
  }

  getBuildingTable(): WaterMeterTable[] {
    let count = 1;
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
    this.waterMeterRepository.getWaterMeters()
        .subscribe(e => this.waterMeters = e);
      this.dataSource.data = this.getBuildingTable();
  }
}
