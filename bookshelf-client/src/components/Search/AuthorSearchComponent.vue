<template>
	<div class="search-wrapper">
		<div class="search-field-container">
			<SearchField
				@search-term-change="searchTermChangeHandler"
				:searchTerm="searchTerm"
				:placeholder="'Add author...'"
			/>
		</div>
		<div v-if="authors.length > 0" class="search__body-container">
			<div class="search__body">
				<div class="search__body-section" v-if="authors.length > 0">
					<div class="search__body-title">Authors</div>
					<div
						class="search__result-item"
						v-for="author in authors"
						:key="author.id"
						@click="() => itemSelectHandler(author.id, author.name)"
					>
						<div class="search__image-container">
							<FallbackImage
								:defaultImage="defaultAuthorImage"
								:source="author.imageUrl"
							/>
						</div>
						<div class="search__result-item-text">
							{{ author.name }}
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import { ref } from "vue";
	import { debounce } from "lodash";
	import axios from "@/config/axios";
	import SearchField from "@/components/Ui/SearchField.vue";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage";
	import defaultAuthorImage from "@/assets/images/rasters/avatar-placeholder.png";

	export default {
		components: {
			SearchField,
			FallbackImage,
		},
		emits: ["author-select"],
		setup: (_, context) => {
			const searchTerm = ref("");
			const authors = ref([]);

			const searchTermChangeHandler = (value) => {
				searchTerm.value = value;
				searchHandler(value);
			};

			const searchHandler = debounce((value) => {
				if (value === "") {
					authors.value = [];
				} else {
					axios.get(`Home/Search?searchString=${value}`).then((response) => {
						authors.value = response.data.authors;
					});
				}
			}, 300);

			const itemSelectHandler = (id, name) => {
				authors.value = [];
				searchTerm.value = "";
				context.emit("author-select", { id, name });
			};

			return {
				searchTermChangeHandler,
				authors,
				defaultAuthorImage,
				itemSelectHandler,
				searchTerm,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MainSearchComponent";
</style>
