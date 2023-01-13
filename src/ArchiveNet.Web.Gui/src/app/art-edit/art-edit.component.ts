import { Component, Input } from '@angular/core';
import { ArtService } from '../art.service';
import { Art } from '../dto/Art';

@Component({
  selector: 'app-art-edit',
  templateUrl: './art-edit.component.html',
  styleUrls: ['./art-edit.component.css'] 
})
export class ArtEditComponent {

	@Input()
	art: Art = new Art;

	resultMessage: string = '';
	// editForm = this.formBuilder.group({
	// 	title: '',
	// 	rating: 0,
	// 	uri: ''
	//   });

    constructor(private artService: ArtService) { }

    update(art: Art) {
		this.resultMessage = "";
        this.artService.update(art).subscribe(result => {
			console.log('Put scene result: ', result);
			this.resultMessage = "Completed successfully." + result;
        }, error => {
			console.log(`There was an issue. ${error._body}.`);
			this.resultMessage = `There was an issue. ${error._body}.`;
        });
	}
}
