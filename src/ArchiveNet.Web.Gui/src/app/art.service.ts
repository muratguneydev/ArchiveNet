import { HttpClient } from '@angular/common/http';
import { Product } from './products';
import { Injectable } from '@angular/core';
import { Art } from './dto/Art';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArtService {
  constructor(
    private http: HttpClient
  ) {}

  getAllArtItems() {
    return this.http.get<Art[]>(`@api-art/Art`);
  }

  getArtItemsByArtist(artistName: string) {
    return this.http.get<Art[]>(`@api-art/Art/${artistName}`);
  }

  getArtItemsByEntryDate(entryDateOffset: number) {
    return this.http.get<Art[]>(`@api-art/Art/GetByDateOffset/${entryDateOffset}`);
  }

  update(art: Art): Observable<any> {
	console.log(art);
	return this.http.put(`@api-art/Art`, art);
	}
}

