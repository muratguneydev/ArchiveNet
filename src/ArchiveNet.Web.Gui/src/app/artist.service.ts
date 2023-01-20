import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Artist } from './dto/Artist';


@Injectable({
	providedIn: 'root'
})
export class ArtistService {
	constructor(
		private http: HttpClient
	) { }

	getAllArtists() {
		return this.http.get<Artist[]>(`@api-art/Artist`);
	}

	getArtist(artistId: number) {
		return this.http.get<Artist>(`@api-art/Artist/${artistId}`);
	}

	update(artist: Artist): Observable<any> {
		console.log(artist);
		return this.http.put(`@api-art/Artist`, artist);
	}
}
