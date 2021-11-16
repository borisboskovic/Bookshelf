<template>
	<div class="reading-item-container">
		<div class="image-container">
			<router-link :to="`/book-details/${id}`">
				<img :src="image" :alt="title" />
			</router-link>
		</div>
		<div class="details-container">
			<div>
				<div class="title">
					<router-link :to="`/book-details/${id}`">
						{{ title }}
					</router-link>
				</div>
				<AuthorLinks :authors="authors" />
			</div>
			<div class="progress">
				<ProgressBar :total="totalPages" :done="pagesRead" />
				<span>{{ progressLabel }}</span>
			</div>
			<div class="button-container">
				<ButtonComponent size="small"> Update progress </ButtonComponent>
			</div>
		</div>
	</div>
</template>

<script>
	import { computed } from "vue";
	import AuthorLinks from "./AuthorLinks.vue";
	import ProgressBar from "@/components/Ui/ProgressBar.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";

	export default {
		components: {
			AuthorLinks,
			ProgressBar,
			ButtonComponent,
		},
		props: {
			id: Number,
			title: String,
			authors: Array,
			image: String,
			totalPages: Number,
			pagesRead: Number,
		},
		setup: (props) => {
			const progressLabel = computed(() => {
				const percentage = ((props.pagesRead / props.totalPages) * 100).toFixed(0);
				return `${props.pagesRead}/${props.totalPages} (${percentage}%)`;
			});

			return {
				progressLabel,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CurrentlyReadingItem";
</style>
