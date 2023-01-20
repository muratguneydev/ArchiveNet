import { Component, Input } from '@angular/core';
import { ArtistService } from '../artist.service';
import { Artist, EmptyArtist } from '../dto/Artist';

@Component({
  selector: 'app-artist-edit',
  templateUrl: './artist-edit.component.html',
  styleUrls: ['./artist-edit.component.css'] 
})
export class ArtistEditComponent {

	@Input()
	artist: Artist = new EmptyArtist;

	resultMessage: string = '';

    constructor(private artistService: ArtistService) { }

    update(artist: Artist) {
		this.resultMessage = "";
        this.artistService.update(artist).subscribe(result => {
			console.log('Put scene result: ', result);
			this.resultMessage = "Completed successfully." + result;
        }, error => {
			console.log(`There was an issue. ${error._body}.`);
			this.resultMessage = `There was an issue. ${error._body}.`;
        });
	}
}
