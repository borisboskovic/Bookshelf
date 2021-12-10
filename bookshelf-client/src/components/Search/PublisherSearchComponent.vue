<template>
	<div class="search-wrapper">
		<div class="search-field-container">
			<SearchField @search-term-change="searchTermChangeHandler" :searchTerm="searchTerm" />
		</div>
		<div v-if="publishers.length > 0" class="search__body-container">
			<div class="search__body">
				<div class="search__body-section" v-if="publishers.length > 0">
					<div
						class="search__result-item"
						v-for="publisher in publishers"
						:key="publisher.id"
						@click="() => itemSelectHandler(publisher.id, publisher.name)"
					>
						<div class="search__result-item-text">
							{{ publisher.name }}
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

	export default {
		components: {
			SearchField,
		},
		emits: ["publisher-select"],
		setup: (_, context) => {
			const searchTerm = ref("");
			const publishers = ref([]);

			const searchTermChangeHandler = (value) => {
				searchTerm.value = value;
				searchHandler(value);
			};

			const searchHandler = debounce((value) => {
				if (value === "") {
					publishers.value = [];
				} else {
					axios.get(`Publisher/Search?searchString=${value}`).then((response) => {
						publishers.value = response.data;
					});
				}
			}, 300);

			const itemSelectHandler = (id, name) => {
				publishers.value = [];
				searchTerm.value = "";
				context.emit("publisher-select", { id, name });
			};

			return {
				searchTermChangeHandler,
				publishers,
				itemSelectHandler,
				searchTerm,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MainSearchComponent";
</style>
