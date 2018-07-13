import { Injectable } from '@angular/core';
import { Building } from '../models/building.model';
import { ConfigService } from '../util/config.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BuildingCreateModel } from '../models/building-create.model';
import { BuildingUpdateModel } from '../models/building-update.model';

@Injectable()
export class BuildingRepository {

    baseUrl = '';
    headers: HttpHeaders = null;

    private buildings: Building[] = [];

    constructor (
        private http: HttpClient,
        private configService: ConfigService
    ) {
        this.baseUrl = configService.getApiURI();
        this.headers = configService.getHeaders();
    }

    getBuildings() {
        return this.http.get<Building[]>(this.baseUrl + 'buildings/getall', { headers: this.headers });
    }

    getBuilding(id: number) {
        return this.http.get<Building>(this.baseUrl + `buildings/getbyid/${id}`, { headers: this.headers });
    }

    addBuilding(building: BuildingCreateModel) {
        const body = JSON.stringify(building);

        return this.http.post(this.baseUrl + 'buildings/add', body, { headers: this.headers});
    }

    updateBuilding(id, building: BuildingUpdateModel) {
        const body = JSON.stringify(building);

        return this.http.put(this.baseUrl + `buildings/UpdateById/${building.Id}`, body, { headers: this.headers});
    }

    deleteBuilding(id: number) {
        return this.http.delete(this.baseUrl + `buildings/delete/${id}`, {headers: this.headers});
    }
}
