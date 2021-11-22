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
				<ButtonComponent @click="showProgressModal" size="small">
					<UpdateProgressModal
						v-if="showingProgressModal"
						:totalPages="totalPages"
						:pagesRead="pagesRead"
						@mouseleave="dismissProgressModal"
						@dismiss="dismissProgressModal"
						@update-progress="updateHandler"
						@finish-book="finishHandler"
					/>
					Update progress
				</ButtonComponent>
			</div>
		</div>
	</div>
</template>

<script>
	import { ref, computed } from "vue";
	import AuthorLinks from "./AuthorLinks.vue";
	import ProgressBar from "@/components/Ui/ProgressBar.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";
	import UpdateProgressModal from "@/components/Ui/Modals/UpdateProgressModal.vue";

	export default {
		components: {
			AuthorLinks,
			ProgressBar,
			ButtonComponent,
			UpdateProgressModal,
		},
		props: {
			id: Number,
			title: String,
			authors: Array,
			image: String,
			totalPages: Number,
			pagesRead: Number,
			dispatch: Function,
		},
		setup: (props) => {
			const showingProgressModal = ref(false);

			const showProgressModal = () => {
				showingProgressModal.value = true;
			};
			const dismissProgressModal = () => {
				showingProgressModal.value = false;
			};

			const progressLabel = computed(() => {
				const percentage = ((props.pagesRead / props.totalPages) * 100).toFixed(0);
				return `${props.pagesRead}/${props.totalPages} (${percentage}%)`;
			});

			const updateHandler = (value) => {
				props.dispatch("currentlyReading/updateProgress", {
					bookIssueId: props.id,
					pagesRead: value,
				});
				dismissProgressModal();
			};

			const finishHandler = () => {
				props.dispatch("currentlyReading/finishBook", props.id);
			};

			return {
				progressLabel,
				showingProgressModal,
				showProgressModal,
				dismissProgressModal,
				updateHandler,
				finishHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CurrentlyReadingItem";
</style>
