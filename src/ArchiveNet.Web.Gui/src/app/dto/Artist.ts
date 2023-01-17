import { EmptyName, Name } from "./Name";
import { EmptyNameCollection, NameCollection } from "./NameCollection";

export class Artist {
	id: number = 0;
	name: Name = new Name('');
	alsoKnownAs: NameCollection = new NameCollection([]);

	constructor(id: number, name: Name, alsoKnownAs: NameCollection) {
		this.id = id;
		this.name = name;
		this.alsoKnownAs = alsoKnownAs;
	}
}

export class EmptyArtist extends Artist {
	constructor() {
		super(0, new EmptyName(), new EmptyNameCollection());
	}
}