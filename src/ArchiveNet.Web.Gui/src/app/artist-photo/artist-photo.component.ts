import { Component, Input } from '@angular/core';
import { BaseUrl } from '../BaseUrl';
import { Artist, EmptyArtist } from '../dto/Artist';
import { EncryptionService } from '../EncryptionService';

@Component({
    selector: 'artist-photo',
	templateUrl: './artist-photo.component.html',
	styleUrls: ['./artist-photo.component.css']
})

export class ArtistPhotoComponent {
	@Input() artist: Artist = new EmptyArtist();
	@Input() bigPhoto: boolean = false;
	@Input() smallPhoto: boolean = false;

    constructor(
		private encryptionService: EncryptionService,
		private baseUrl: BaseUrl) { }

	public get imageUrl() { return `${this.baseUrl.artistImage}/${this.getNamePart()}_${this.artist.id}.jpg`; }

	private getNamePart() {
		return this.encryptionService.encrypt(this.artist.name.value);
	}
}


