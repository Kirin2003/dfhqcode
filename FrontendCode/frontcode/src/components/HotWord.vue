<template>
  <div class="common-layout">
    <el-container>
      <AsidePart1 />
      <el-container>
        <el-header>
          <el-input v-model="essaySearched" placeholder="论文信息">
            <template #prepend>
              <el-select
                v-model="select"
                placeholder="相关度"
                default-first-option="true"
                width="125px"
              >
                <el-option label="相关度" value="revelance" />
                <el-option label="热度" value="hotnum" />
                <el-option label="时间" value="time" />
              </el-select>
            </template>
            <template #append>
              <el-button>
                <el-icon><search /></el-icon>
              </el-button>
            </template>
          </el-input>
        </el-header>
        <p class="sort" :sort="Sort">"{{ Sort }}"论文</p>
        <EssayList />
      </el-container>
    </el-container>
  </div>
</template>
<script>
import { Search } from "@element-plus/icons";
import router from "@/router/index";
import store from "@/store/";
import AsidePart1 from "./AsidePart1.vue";
import EssayList from "./EssayList.vue";
export default {
  name: "HotWord",
  components: {
    Search,
    AsidePart1,
    EssayList,
  },
  data() {
    return {
      select: "",
      AllEssays: [
        {
          title:
            "Image-to-Image Translation with Conditional Adversarial Networks",
          writers: "Phillip Isola, Jun-Yan Zhu, Tinghui Zhou, Alexei A. Efros",
        },
        {
          title:
            "Unpaired Image-to-Image Translation Using Cycle-Consistent Adversarial Networks",
          writers: "Jun-Yan Zhu, Taesung Park Alexei A. Efros 2017",
        },
        {
          title:
            "Very Deep Convolutional Networks for Large-Scale Image Recognition",
          writers: "K. Simonyan, Andrew Zisserman",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
      ], //五个论文一组的数组
      Essays: [
        {
          Id: "134153413",
          title:
            "Image-to-Image Translation with Conditional Adversarial Networks",
          Authors: "Phillip Isola, Jun-Yan Zhu, Tinghui Zhou, Alexei A. Efros",
        },
        {
          title:
            "Unpaired Image-to-Image Translation Using Cycle-Consistent Adversarial Networks",
          writers: "Jun-Yan Zhu, Taesung Park Alexei A. Efros 2017",
        },
        {
          title:
            "Very Deep Convolutional Networks for Large-Scale Image Recognition",
          writers: "K. Simonyan, Andrew Zisserman",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
        {
          title:
            "Photo-Realistic Single Image Super-Resolution Using a Generative Adversarial Network",
          writers: "C. Ledig, Lucas Theis, Ferenc Huszár, Jose",
        },
      ], //一组论文
      Sort: "default",
      essaySelected: {},
      //EssayNum: this.Essays.length * 5,
      essaySearched: "",
    };
  },
  methods: {
    RouteToEssayInfo(row) {
      this.essaySelected = row;
      store.commit("SelectEssay", this.essaySelected);
      //还要传递论文信息
      router.push({
        name: "Essay",
        params: { essayId: this.essaySelected.Id },
      });
    },
  },
};
</script>
<style scoped>
.essays {
  font-size: 20px;
}
.sort {
  font-size: 20px;
  font-weight: bold;
  margin-top: 0px;
}
.el-pagination {
  padding-left: 400px;
  padding-top: 20px;
  --el-pagination-font-size: 17px;
}
</style>
