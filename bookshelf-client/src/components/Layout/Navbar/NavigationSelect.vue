<template>
	<div class="navigation-select-container" @mouseleave="dismissDropdownHandler">
		<div class="navigation-select__header" @click="toggleDropdown">
			<div>More</div>
			<i class="fa fa-caret-down fa-lg" aria-hidden="true" v-if="!dropdownShown"></i>
			<i class="fa fa-caret-up fa-lg" aria-hidden="true" v-else></i>
		</div>
		<div class="navigation-select__body-container">
			<div class="navigation-select__body" v-if="dropdownShown">
				<router-link
					@click="dismissDropdownHandler"
					to="/create-author"
					class="navigation-select__list-item"
					>Add Author</router-link
				>
				<router-link
					@click="dismissDropdownHandler"
					to="/create-book"
					class="navigation-select__list-item"
					>Add Book</router-link
				>
				<router-link
					@click="dismissDropdownHandler"
					to="/create-book-issue"
					class="navigation-select__list-item"
					>Add Book Issue</router-link
				>
			</div>
		</div>
	</div>
</template>

<script>
	import { ref } from "vue";

	export default {
		setup: () => {
			const dropdownShown = ref(false);

			const toggleDropdown = () => {
				dropdownShown.value = !dropdownShown.value;
			};

			const dismissDropdownHandler = () => {
				dropdownShown.value = false;
			};

			return {
				dropdownShown,
				toggleDropdown,
				dismissDropdownHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavigationSelect";
</style>
